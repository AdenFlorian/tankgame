using UnityEngine;

public class InputMaster : Master {

	public bool mouse0 { get; private set; }

	public InputMaster() {
		Debug.Log(GetType().Name + " Loaded!");
	}

	protected override void MasterUpdate() {
		ProcessInput();
	}

	private void ProcessInput() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			if (Screen.lockCursor == false) {
				Screen.lockCursor = true;
				mouse0 = false;
			} else {
				mouse0 = true;
			}
		}
	}
}
