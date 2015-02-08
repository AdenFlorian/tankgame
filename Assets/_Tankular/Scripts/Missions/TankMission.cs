using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankMission : Mission {

	protected Tank playerTank;

	protected int enemyTankCount = 0;

	public TankMission() {
		triggers[MissionEvent.ActorDeath] = OnActorDeath;
		triggers[MissionEvent.PlayerDeath] = OnPlayerDeath;
		triggers[MissionEvent.Load] = OnMissionLoad;
		UnityEngine.Random.seed = 42 * DateTime.Now.Millisecond;
	}

	protected virtual void OnPlayerDeath() {
		GameMaster.isPlayerDead = true;
		MissionMaster.UnloadMission();
	}

	protected virtual void OnMissionLoad() {
		Master.gameMaster.Reset();
		// Spawn AmmoPool
		SpawnMaster.SpawnActor<AmmoPool>();

		// Spawn player
		playerTank = SpawnMaster.SpawnActor<Tank>(ControlledBy.PlayerLocal);
	}

	protected virtual void OnActorDeath() {
		enemyTankCount--;
		// Check for winning conditions
		if (enemyTankCount <= 0) {
			GameMaster.isPlayerWin = true;
			MissionMaster.UnloadMission();
		}
	}
}
