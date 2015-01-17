using System.Collections;
using UnityEngine;

public class MissionMaster : Master {

	public static Mission currentMission { get; private set; }

	public MissionMaster() {
	}

	~MissionMaster() {

	}

	public static void ReportMissionEvent(MissionEvent missionEvent) {
		currentMission.triggers[missionEvent].Invoke();
	}

	public static void LoadMission<T>() where T : Mission, new() {
		currentMission = new T();
		ReportMissionEvent(MissionEvent.Load);
	}
}

public enum MissionEvent {
	Load,
	ActorDeath,
	ActorSpawn
}
