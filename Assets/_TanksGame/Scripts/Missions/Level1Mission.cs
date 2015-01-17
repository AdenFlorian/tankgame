using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Mission : Mission {

	Tank playerTank;

	int enemyTankCount = 0;

	int wave = 0;
	int waves = 4;
	int increaseTanksBy = 2;
	int tanksNextWave = 2;

	public Level1Mission() {
		triggers[MissionEvent.ActorDeath] = OnActorDeath;
		triggers[MissionEvent.PlayerDeath] = OnPlayerDeath;
		triggers[MissionEvent.Load] = OnMissionLoad;
		UnityEngine.Random.seed = 42 * DateTime.Now.Millisecond;
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

		// Spawn player and two tanks
		playerTank = SpawnMaster.SpawnActor<Tank>(ControlledBy.PlayerLocal);

		// Spawn first wave
		SpawnNextWave();
	}

	private void OnActorDeath() {
		enemyTankCount--;
		// Check for winning conditions
		if (enemyTankCount <= 0) {
			if (wave == waves) {
				GameMaster.isPlayerWin = true;
				MissionMaster.UnloadMission();
			} else {
				SpawnNextWave();
			}
		}
	}

	private void SpawnNextWave() {
		for (int i = 0; i < tanksNextWave; i++) {
			Tank newTank = SpawnMaster.SpawnActor<Tank>(ControlledBy.AI, new Vector3(UnityEngine.Random.Range(-25, 25), 1f, UnityEngine.Random.Range(-30, 30)));
			newTank.controller.rulesOfEngage = RulesOfEngagement.OpenFire;
			newTank.controller.state = (TankMoveOrders)UnityEngine.Random.Range(1, 4);
			newTank.controller.nextWaypoint = playerTank.transform;
			enemyTankCount++;
		}
		wave++;
		tanksNextWave += increaseTanksBy;
	}
}
