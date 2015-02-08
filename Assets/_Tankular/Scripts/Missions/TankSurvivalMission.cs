using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSurvivalMission : TankMission {

	int wave = 0;
	int waves = 4;
	int increaseTanksBy = 2;
	int tanksNextWave = 2;

	public TankSurvivalMission() {
		triggers[MissionEvent.ActorDeath] = OnActorDeath;
		triggers[MissionEvent.PlayerDeath] = OnPlayerDeath;
		triggers[MissionEvent.Load] = OnMissionLoad;
		UnityEngine.Random.seed = 42 * DateTime.Now.Millisecond;
	}

	protected void OnPlayerDeath() {
		GameMaster.isPlayerDead = true;
		MissionMaster.UnloadMission();
	}

	protected void OnMissionLoad() {

		Master.gameMaster.Reset();
		// Spawn AmmoPool
		SpawnMaster.SpawnActor<AmmoPool>();

		// Spawn player
		playerTank = SpawnMaster.SpawnActor<Tank>(ControlledBy.PlayerLocal);

		// Spawn first wave
		SpawnNextWave();
	}

	protected void OnActorDeath() {
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

	protected void SpawnNextWave() {
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
