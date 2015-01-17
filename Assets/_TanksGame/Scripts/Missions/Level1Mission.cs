using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Mission : Mission {

	Tank playerTank;

	int enemyTankCount = 0;

	public Level1Mission() {
		triggers[MissionEvent.ActorDeath] = OnEnemyTankDeath;
		triggers[MissionEvent.Load] = OnMissionLoad;
		UnityEngine.Random.seed = 42 * DateTime.Now.Millisecond;
	}

	private void OnMissionLoad() {
		// Spawn player and two tanks
		playerTank = SpawnMaster.SpawnActor<Tank>(ControlledBy.PlayerLocal);

		Vector3[] newTankPositions = new Vector3[] {
			new Vector3(-5, 0, 13),
			new Vector3(-6, 0, 15)
		};

		Tank[] newTanks = SpawnMaster.SpawnActors<Tank>(newTankPositions.Length, ControlledBy.AI, newTankPositions);
		enemyTankCount += newTankPositions.Length;

		foreach (Tank tank in newTanks) {
			tank.controller.rulesOfEngage = RulesOfEngagement.OpenFire;
			tank.controller.state = (TankMoveOrders)UnityEngine.Random.Range(1, 4);
			tank.controller.nextWaypoint = playerTank.transform;
		}
	}

	private void OnEnemyTankDeath() {
		enemyTankCount--;
		// Check for winning conditions
		if (enemyTankCount <= 0) {
			// Mission complete!
			MissionMaster.LoadMission<Level2Mission>();
		}
	}
}
