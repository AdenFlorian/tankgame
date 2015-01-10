using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMission : Mission {

	public MainMission() {
		triggers.Add(MissionEvent.ActorDeath, new List<Action>());
		triggers[MissionEvent.ActorDeath].Add(OnEnemyTankDeath);
	}

	private void OnEnemyTankDeath() {
		Master.spawnMaster.SpawnActor("Tank", ControlledBy.AI, new Vector3(17f, 0f, 15f));
		Master.spawnMaster.SpawnActor("Tank", ControlledBy.AI, new Vector3(-23f, 0f, 19f));
	}
}
