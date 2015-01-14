using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMission : Mission {

	Tank playerTank;

	public MainMission() {
		triggers[MissionEvent.ActorDeath] = OnEnemyTankDeath;
		triggers[MissionEvent.Load] = OnMissionLoad;
		UnityEngine.Random.seed = 42 * DateTime.Now.Millisecond;
	}

	private void OnEnemyTankDeath() {
		for (int i = 0; i < 2; i++) {
			SpawnMaster.SpawnActor(typeof(Tank), ControlledBy.AI, new Vector3(UnityEngine.Random.Range(-30, 30), 1f, UnityEngine.Random.Range(-30, 30)));
		}
	}

	private void OnMissionLoad() {
		playerTank = SpawnMaster.SpawnActor<Tank>(ControlledBy.PlayerLocal);

		Vector3[] newTankPositions = new Vector3[] {
			new Vector3(-5, 0, 13),
			new Vector3(6, 0, 15),
			new Vector3(-6, 0, -19),
			new Vector3(6, 0, -3),
			new Vector3(36, 0, -15),
			new Vector3(46, 0, 0),
			new Vector3(56, 0, -5),
			new Vector3(12, 0, -5),
			new Vector3(12, 0, 1),
			new Vector3(16, 0, 17),
			new Vector3(-12, 0, -5)
		};

		Tank[] newTanks = SpawnMaster.SpawnActors<Tank>(newTankPositions.Length, ControlledBy.AI, newTankPositions);

		foreach (Tank tank in newTanks) {
			tank.controller.rulesOfEngage = RulesOfEngagement.OpenFire;
			tank.controller.state = (TankMoveOrders)UnityEngine.Random.Range(1, 4);
			tank.controller.nextWaypoint = playerTank.transform;
		}
	}
}
