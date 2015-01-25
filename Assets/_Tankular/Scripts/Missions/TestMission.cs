using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMission : Mission {

	Tank playerTank;

	public TestMission() {
		triggers[MissionEvent.Load] = OnMissionLoad;
	}

	private void OnPlayerDeath() {
		GameMaster.isPlayerDead = true;
		MissionMaster.UnloadMission();
	}

	private void OnMissionLoad() {
		GameMaster.isPlayerDead = false;
		GameMaster.isPlayerWin = false;

		// Spawn AmmoPool
		SpawnMaster.SpawnActor<AmmoPool>();

		// Spawn player
		playerTank = SpawnMaster.SpawnActor<Tank>(ControlledBy.PlayerLocal);

		// Spawn first wave
	}
}
