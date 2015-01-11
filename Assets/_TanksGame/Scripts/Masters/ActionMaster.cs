using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Responsible for mapping ActionCodes to bb_KeyCodes and repsonding to requests for action states
/// Utilizes UnityEngine.Input, but allows for custom actions that can be modified at runtime
/// To use, call one of the three 'InputFunctions'; They work similar to Unity's Input class
/// To make your own actions, add them to the ActionCode enum
/// Set default keys in AddDefaultKeyCodes()
/// Users can change the keys for actions in \Bamboo\Keybindings\Keybindings.txt (This path can be changed at configLocalPath)
/// All actions will be written to the config, whether they have a default keycode or not
/// </summary>
public class ActionMaster : Master {
	private string configPath;

	// Holds the bb_KeyCodes for each ActionCode
	private Dictionary<ActionCode, List<bb_KeyCode>> keyCodesFor = new Dictionary<ActionCode, List<bb_KeyCode>>();

	private List<ActionCode> missingActionCodes = new List<ActionCode>();

	/// <summary>
	///
	/// </summary>
	/// <param name="relativeCfgFilePath">Path to keybindings config file relative to Application.dataPath (Assets folder)</param>
	public ActionMaster(string relativeCfgFilePath = @"Config/KeyBindings.txt") {
		configPath = Application.dataPath + "/" + relativeCfgFilePath;
		Init();
	}

	private void Init() {
		// Fill currentKeysDict with ActionCodes and empty KeyCode Lists
		FillCurrentKeysWithEnum();

		// Add some default keys
		AddDefaultKeyCodes();

#if !UNITY_WEBPLAYER
		// Check for current key config file
		if (File.Exists(configPath)) {
			Debug.Log("Keybindings.txt found! Reading config into keys dictionary...");

			// Fill current keys dict by reading current keybinds config file
			ReadConfigToDict();
		} else {
			Debug.Log("Keybindings.txt NOT found! Generating config from default keys...");

			// Create Config folder if doesn't exist
			Directory.CreateDirectory(Path.GetDirectoryName(configPath));

			// Generate keybinds config
			WriteDictToConfig(keyCodesFor);
		}
#endif
	}

	#region Dictionary Methods

	/// <summary>
	/// Clears keys for given action
	/// </summary>
	/// <param name="action">The action to clear</param>
	public void ClearAction(ActionCode action) {
		keyCodesFor[action].Clear();
	}

	/// <summary>
	/// Resets keymappings to defaults
	/// </summary>
	public void ResetAllActionsToDefaultKeys() {
		keyCodesFor.Clear();

		FillCurrentKeysWithEnum();

		AddDefaultKeyCodes();
	}

	/// <summary>
	/// Sets the given action's keys to the given keyList
	/// </summary>
	/// <param name="action">InputAction to modify</param>
	/// <param name="keyList">List of InputCodes to assign to the given InputAction</param>
	public void BindKeyToAction(ActionCode action, params bb_KeyCode[] inputKeys) {
		List<bb_KeyCode> tempList = new List<bb_KeyCode>();

		foreach (var item in inputKeys) {
			tempList.Add(item);
		}

		// Change keyList for given action in the current keybindings dictionary
		keyCodesFor[action] = tempList;
	}

	/// <summary>
	/// Fills currentKeysDict with action codes and empty key lists
	/// </summary>
	private void FillCurrentKeysWithEnum() {
		ActionCode[] values = (ActionCode[])Enum.GetValues(typeof(ActionCode));

		foreach (ActionCode action in values) {
			List<bb_KeyCode> tempList1 = new List<bb_KeyCode>();

			keyCodesFor.Add(action, tempList1);

			missingActionCodes.Add(action);
		}
	}

	private void AddDefaultKeyCodes() {
		// Weapon Actions
		keyCodesFor[ActionCode.PrimaryFire].Add(bb_KeyCode.Mouse0);
		keyCodesFor[ActionCode.SecondaryFire].Add(bb_KeyCode.Mouse1);

		// Movement Actions
		keyCodesFor[ActionCode.MoveForward].Add(bb_KeyCode.W);
		keyCodesFor[ActionCode.MoveBackward].Add(bb_KeyCode.S);
		keyCodesFor[ActionCode.TurnLeft].Add(bb_KeyCode.A);
		keyCodesFor[ActionCode.TurnRight].Add(bb_KeyCode.D);
	}

	#endregion Dictionary Methods

	#region ConfigFunctions

	private void WriteDictToConfig(Dictionary<ActionCode, List<bb_KeyCode>> dict) {
		using (StreamWriter config = new StreamWriter(configPath, false)) {
			config.WriteLine("# Commented lines start with a '#'");
			config.WriteLine("# Put action name on left of '='");
			config.WriteLine("# Put inputs on left side of '=' and multiple inputs separated by commas ','");

			foreach (KeyValuePair<ActionCode, List<bb_KeyCode>> pair in dict) {
				string line = pair.Key.ToString() + "=";

				for (int i = 0; i < pair.Value.Count; i++) {
					if (i < pair.Value.Count - 1) {
						line += pair.Value[i].ToString() + ",";
					} else {
						line += pair.Value[i].ToString();
					}
				}

				config.WriteLine(line);
			}
		}
	}

	// Returns a dictionary filled with bindings read from path
	private void ReadConfigToDict() {
		//currentKeysDict.Clear();

		string[] lines = System.IO.File.ReadAllLines(configPath);
		string[] codes;

		for (int i = 0; i < lines.Length; i++) {

			// Skip lines that start with a '#', so those lines are effectively commented out
			if (lines[i].StartsWith("#")) {
				continue;
			} else if (lines[i].Contains("=") == false) {	// Erases lines with no #'s nor ='s
				lines[i] = "";
				continue;
			}

			// Clear out spaces and tab characters
			lines[i].Replace(" ", string.Empty);	// Removes all spaces in the string, leaving newlines and tabs intact
			lines[i].Replace("\t", string.Empty);	// Removes all tab characters from string, leaving newlines intact

			string[] actionAndCodes = lines[i].Split('=');		// Splits line into two (or more) strings delimited by '=', where the first string should be the action name, and the rest are the KeyCodes

			ActionCode action = (ActionCode)Enum.Parse(typeof(ActionCode), actionAndCodes[0]);

			missingActionCodes.Remove(action);

			if (actionAndCodes[1] != "") {
				codes = actionAndCodes[1].Split(',');	// Splits the second string from the previous split into multiple strings delimited by a ','
			} else {
				codes = null;
			}

			List<bb_KeyCode> workingInputCodeList = new List<bb_KeyCode>();	// Stores a working input code list

			// Parses input codes and adds to working input code list
			if (codes != null) {
				for (int j = 0; j < codes.Length; j++) {
					if (codes[j] != "") {
						workingInputCodeList.Add((bb_KeyCode)Enum.Parse(typeof(bb_KeyCode), codes[j]));
					}
				}
			}

			// add the input code list to the existing action
			keyCodesFor[action] = workingInputCodeList;
		}

		RewriteConfig(lines);
	}

	private void RewriteConfig(string[] linesToWrite) {
		// Opens keybinds config file
		using (StreamWriter configWriter = new StreamWriter(configPath, false)) {
			foreach (string line in linesToWrite) {
				if (line.Length > 1) {
					configWriter.WriteLine(line);
				}
			}

			foreach (ActionCode action in missingActionCodes) {
				configWriter.WriteLine(action + "=");
			}
		}
	}

	#endregion ConfigFunctions

	#region InputFunctions

	enum InputState {
		Up,
		Down,
		Any
	}

	public bool GetActionDown(ActionCode action) {
		return GetAction(action, InputState.Down);
	}

	public bool GetActionUp(ActionCode action) {
		return GetAction(action, InputState.Up);
	}

	public bool GetAction(ActionCode action) {
		return GetAction(action, InputState.Any);
	}

	private bool GetAction(ActionCode action, InputState state) {
		foreach (bb_KeyCode keyCode in keyCodesFor[action]) {
			KeyCode unityKeyCode = (KeyCode)keyCode;
			if ((int)keyCode < 1000) {
				if (state == InputState.Any && Input.GetKey(unityKeyCode)) {
					return true;
				} else if (state == InputState.Up && Input.GetKeyUp(unityKeyCode)) {
					return true;
				} else if (state == InputState.Down && Input.GetKeyDown(unityKeyCode)) {
					return true;
				}
			} else if (keyCode == bb_KeyCode.MouseScrollUp) {
				if (Input.GetAxis("Mouse ScrollWheel") > 0) {
					return true;
				}
			} else if (keyCode == bb_KeyCode.MouseScrollDown) {
				if (Input.GetAxis("Mouse ScrollWheel") < 0) {
					return true;
				}
			}
		}

		return false;
	}

	public float GetAxis(AxisCode axisCode) {
		switch (axisCode) {
			case AxisCode.LookHorizontal:
				return Input.GetAxis("Mouse X");
			case AxisCode.LookVertical:
				return Input.GetAxis("Mouse Y");
			default:
				throw new NotImplementedException(
					"No case defined for " + axisCode + " in " +
					MethodBase.GetCurrentMethod().Name);
		}
	}

	#endregion InputFunctions
}

#region Enumerators

/* ActionCodes with defaults

/// <summary>
/// Available actions to assign keyLists to
/// Each ActionCode contains the value of its default InputCode
/// All Action
/// </summary>
public enum ActionCode {
	ChangeTacticalMode,
	ToggleDevConsole,

	// **WEAPON** //
	PrimaryFire,
	SecondaryFire,
	RaiseLowerWeapon,
	ReloadWeapon,

	// **MOVEMENT** //
	MoveForward,
	MoveBackward,
	StrafeLeft,
	StrafeRight,
	Jump,
	Crouch,
	Prone,
	Stand,
	SprintHold,
	WalkHold,
	WalkBackHold,
	MoveSpeedIncrement,
	MoveSpeedDecrement,

	// **INENTORY** //
	Inventory_NextItem,
	Inventory_PrevItem,
	Inventory_Open
}
*/

// OldActionCodes with defaults
// Anything set to over 9000 has no default keycode
/// <summary>
/// Available actions to assign keyLists to
/// Each ActionCode contains the value of its default InputCode
/// All Action
/// </summary>
public enum ActionCode {
	MoveForward,
	MoveBackward,
	TurnLeft,
	TurnRight,
	PrimaryFire,
	SecondaryFire
}

public enum AxisCode {
	LookHorizontal,
	LookVertical
}

/// <summary>
/// A near copy of UnityEngine.KeyCode with a few additions
/// Used internally
/// </summary>
public enum bb_KeyCode {
	None = 0,
	Backspace = 8,
	Tab = 9,
	Clear = 12,
	Return = 13,
	Pause = 19,
	Escape = 27,
	Space = 32,
	Exclaim = 33,
	DoubleQuote = 34,
	Hash = 35,
	Dollar = 36,
	Ampersand = 38,
	Quote = 39,
	LeftParen = 40,
	RightParen = 41,
	Asterisk = 42,
	Plus = 43,
	Comma = 44,
	Minus = 45,
	Period = 46,
	Slash = 47,
	Alpha0 = 48,
	Alpha1 = 49,
	Alpha2 = 50,
	Alpha3 = 51,
	Alpha4 = 52,
	Alpha5 = 53,
	Alpha6 = 54,
	Alpha7 = 55,
	Alpha8 = 56,
	Alpha9 = 57,
	Colon = 58,
	Semicolon = 59,
	Less = 60,
	Equals = 61,
	Greater = 62,
	Question = 63,
	At = 64,
	LeftBracket = 91,
	Backslash = 92,
	RightBracket = 93,
	Caret = 94,
	Underscore = 95,
	BackQuote = 96,
	A = 97,
	B = 98,
	C = 99,
	D = 100,
	E = 101,
	F = 102,
	G = 103,
	H = 104,
	I = 105,
	J = 106,
	K = 107,
	L = 108,
	M = 109,
	N = 110,
	O = 111,
	P = 112,
	Q = 113,
	R = 114,
	S = 115,
	T = 116,
	U = 117,
	V = 118,
	W = 119,
	X = 120,
	Y = 121,
	Z = 122,
	Delete = 127,
	Keypad0 = 256,
	Keypad1 = 257,
	Keypad2 = 258,
	Keypad3 = 259,
	Keypad4 = 260,
	Keypad5 = 261,
	Keypad6 = 262,
	Keypad7 = 263,
	Keypad8 = 264,
	Keypad9 = 265,
	KeypadPeriod = 266,
	KeypadDivide = 267,
	KeypadMultiply = 268,
	KeypadMinus = 269,
	KeypadPlus = 270,
	KeypadEnter = 271,
	KeypadEquals = 272,
	UpArrow = 273,
	DownArrow = 274,
	RightArrow = 275,
	LeftArrow = 276,
	Insert = 277,
	Home = 278,
	End = 279,
	PageUp = 280,
	PageDown = 281,
	F1 = 282,
	F2 = 283,
	F3 = 284,
	F4 = 285,
	F5 = 286,
	F6 = 287,
	F7 = 288,
	F8 = 289,
	F9 = 290,
	F10 = 291,
	F11 = 292,
	F12 = 293,
	F13 = 294,
	F14 = 295,
	F15 = 296,
	Numlock = 300,
	CapsLock = 301,
	ScrollLock = 302,
	RightShift = 303,
	LeftShift = 304,
	RightControl = 305,
	LeftControl = 306,
	RightAlt = 307,
	LeftAlt = 308,
	RightApple = 309,
	RightCommand = 309,
	LeftApple = 310,
	LeftCommand = 310,
	LeftWindows = 311,
	RightWindows = 312,
	AltGr = 313,
	Help = 315,
	Print = 316,
	SysReq = 317,
	Break = 318,
	Menu = 319,
	Mouse0 = 323,
	Mouse1 = 324,
	Mouse2 = 325,
	Mouse3 = 326,
	Mouse4 = 327,
	Mouse5 = 328,
	Mouse6 = 329,
	JoystickButton0 = 330,
	JoystickButton1 = 331,
	JoystickButton2 = 332,
	JoystickButton3 = 333,
	JoystickButton4 = 334,
	JoystickButton5 = 335,
	JoystickButton6 = 336,
	JoystickButton7 = 337,
	JoystickButton8 = 338,
	JoystickButton9 = 339,
	JoystickButton10 = 340,
	JoystickButton11 = 341,
	JoystickButton12 = 342,
	JoystickButton13 = 343,
	JoystickButton14 = 344,
	JoystickButton15 = 345,
	JoystickButton16 = 346,
	JoystickButton17 = 347,
	JoystickButton18 = 348,
	JoystickButton19 = 349,
	Joystick1Button0 = 350,
	Joystick1Button1 = 351,
	Joystick1Button2 = 352,
	Joystick1Button3 = 353,
	Joystick1Button4 = 354,
	Joystick1Button5 = 355,
	Joystick1Button6 = 356,
	Joystick1Button7 = 357,
	Joystick1Button8 = 358,
	Joystick1Button9 = 359,
	Joystick1Button10 = 360,
	Joystick1Button11 = 361,
	Joystick1Button12 = 362,
	Joystick1Button13 = 363,
	Joystick1Button14 = 364,
	Joystick1Button15 = 365,
	Joystick1Button16 = 366,
	Joystick1Button17 = 367,
	Joystick1Button18 = 368,
	Joystick1Button19 = 369,
	Joystick2Button0 = 370,
	Joystick2Button1 = 371,
	Joystick2Button2 = 372,
	Joystick2Button3 = 373,
	Joystick2Button4 = 374,
	Joystick2Button5 = 375,
	Joystick2Button6 = 376,
	Joystick2Button7 = 377,
	Joystick2Button8 = 378,
	Joystick2Button9 = 379,
	Joystick2Button10 = 380,
	Joystick2Button11 = 381,
	Joystick2Button12 = 382,
	Joystick2Button13 = 383,
	Joystick2Button14 = 384,
	Joystick2Button15 = 385,
	Joystick2Button16 = 386,
	Joystick2Button17 = 387,
	Joystick2Button18 = 388,
	Joystick2Button19 = 389,
	Joystick3Button0 = 390,
	Joystick3Button1 = 391,
	Joystick3Button2 = 392,
	Joystick3Button3 = 393,
	Joystick3Button4 = 394,
	Joystick3Button5 = 395,
	Joystick3Button6 = 396,
	Joystick3Button7 = 397,
	Joystick3Button8 = 398,
	Joystick3Button9 = 399,
	Joystick3Button10 = 400,
	Joystick3Button11 = 401,
	Joystick3Button12 = 402,
	Joystick3Button13 = 403,
	Joystick3Button14 = 404,
	Joystick3Button15 = 405,
	Joystick3Button16 = 406,
	Joystick3Button17 = 407,
	Joystick3Button18 = 408,
	Joystick3Button19 = 409,
	Joystick4Button0 = 410,
	Joystick4Button1 = 411,
	Joystick4Button2 = 412,
	Joystick4Button3 = 413,
	Joystick4Button4 = 414,
	Joystick4Button5 = 415,
	Joystick4Button6 = 416,
	Joystick4Button7 = 417,
	Joystick4Button8 = 418,
	Joystick4Button9 = 419,
	Joystick4Button10 = 420,
	Joystick4Button11 = 421,
	Joystick4Button12 = 422,
	Joystick4Button13 = 423,
	Joystick4Button14 = 424,
	Joystick4Button15 = 425,
	Joystick4Button16 = 426,
	Joystick4Button17 = 427,
	Joystick4Button18 = 428,
	Joystick4Button19 = 429,
	MouseScrollUp = 1000,
	MouseScrollDown = 1001
}

#endregion Enumerators
