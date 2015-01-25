using System.Collections;
using UnityEngine;

public class GamePlaying : GameState {
	public override void Enter() {
		// Load mission
		MissionMaster.LoadMission<TestMission>();
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
		}
		return null;
	}
}
