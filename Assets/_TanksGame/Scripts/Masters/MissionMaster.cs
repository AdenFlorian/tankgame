using System.Collections;
using UnityEngine;

public class MissionMaster : Master {

	public static Mission currentMission { get; private set; }
	static bool isMissionLoaded = false;

	public MissionMaster() {
	}

	~MissionMaster() {

	}

	public static void ReportMissionEvent(MissionEvent missionEvent) {
		if (isMissionLoaded) {
			currentMission.triggers[missionEvent].Invoke();
		}
	}

	public static void LoadMission<T>() where T : Mission, new() {
		// Cleanup previous mission
		// Destroy all Actors in scene
		//SpawnMaster.DespawnAllActors();
		// Load new mission
		currentMission = new T();
		isMissionLoaded = true;
		ReportMissionEvent(MissionEvent.Load);
	}

	public static void UnloadMission() {
		currentMission = null;
		isMissionLoaded = false;
	}
}

public enum MissionEvent {
	Load,
	ActorDeath,
	ActorSpawn,
	PlayerDeath,
	Complete
}
