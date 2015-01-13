using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMission : Mission {

	public MainMission() {
		triggers[MissionEvent.ActorDeath] = OnEnemyTankDeath;
		triggers[MissionEvent.Load] = OnMissionLoad;
		UnityEngine.Random.seed = 42;
	}

	private void OnEnemyTankDeath() {
		for (int i = 0; i < 2; i++) {
			SpawnMaster.SpawnActor(typeof(Tank), ControlledBy.AI, new Vector3(UnityEngine.Random.Range(-30, 30), 1f, UnityEngine.Random.Range(-30, 30)));
		}
	}

	private void OnMissionLoad() {
		SpawnMaster.SpawnActor(typeof(Tank), ControlledBy.Player);

		Vector3[] newTankPositions = new Vector3[] {
			new Vector3(-5, 0, 13),
			new Vector3(6, 0, 15),
			new Vector3(-6, 0, -19),
			new Vector3(6, 0, -3),
			new Vector3(36, 0, -15),
			new Vector3(46, 0, 0),
			new Vector3(56, 0, -5)
		};

		SpawnMaster.SpawnActors(typeof(Tank), newTankPositions.Length, ControlledBy.AI, newTankPositions);
	}
}
