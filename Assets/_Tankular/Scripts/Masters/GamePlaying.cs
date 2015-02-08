using System.Collections;
using UnityEngine;

public class GamePlaying : GameState {

	bool paused = false;
	GameObject menu;

	public override void Enter() {
		// Load mission
		//MissionMaster.LoadMission<TestMission>();
		MissionMaster.LoadMission<TankSurvivalMission>();
	}

	public override void Exit() {
		// Clean up mission
		SpawnMaster.DespawnAllActors();
	}

	public override GameState Update() {
		if (GameMaster.isPlayerDead) {
			return new GameMenu();
		} else if (GameMaster.isPlayerWin) {
			return new GameMenu();
		} else if (GameMaster.isPlayerQuit) {
			return new GameMenu();
		}

		if (!paused && Input.GetKeyDown(KeyCode.Escape)) {
			Screen.lockCursor = false;
			// Pause Game if playing
			Master.gameMaster.Pause();
			menu = GameObject.Instantiate(Resources.Load<GameObject>("PauseMenu")) as GameObject;
			paused = true;
		} else if (paused) {
			if (ActionMaster.GetActionDown(ActionCode.PrimaryFire)) {
				// Resume
				Master.gameMaster.Resume();
				GameObject.Destroy(menu);
				paused = false;
			} else if (Input.GetKeyDown(KeyCode.Q)) {
				// Quit
				GameObject.Destroy(menu);
				Master.gameMaster.Resume();
				paused = false;
				GameMaster.isPlayerQuit = true;
			}
		}
		return null;
	}
}
