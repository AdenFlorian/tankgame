using System.Collections;
using UnityEngine;

public class GameMenu : GameState {
	GameObject menu;
	public override void Enter() {
		//ShowMenu
		menu = GameObject.Instantiate(Resources.Load<GameObject>("MainMenu")) as GameObject;

		if (GameMaster.isPlayerWin) {
			// Show success text
			menu.transform.FindChild("Canvas/TextSuccess").gameObject.SetActive(true);
		} else if (GameMaster.isPlayerDead) {
			// rekt
			menu.transform.FindChild("Canvas/TextFailure").gameObject.SetActive(true);
		}
	}
	public override void Exit() {
		//HideMenu
		GameObject.Destroy(menu);
	}
	public override GameState Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			return new GamePlaying();
		}
		return null;
	}
}
