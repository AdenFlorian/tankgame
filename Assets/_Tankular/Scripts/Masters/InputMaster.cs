using UnityEngine;

public class InputMaster : Master {

	public InputMaster() {
		Debug.Log(GetType().Name + " Loaded!");
	}

	protected override void MasterUpdate() {
		ProcessInput();
	}

	private void ProcessInput() {
		if (Screen.lockCursor == false) {
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				Screen.lockCursor = true;
			}
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			Screen.lockCursor = false;
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			// Kill all enemy tanks
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			// Kill player tank
		}
	}
}
