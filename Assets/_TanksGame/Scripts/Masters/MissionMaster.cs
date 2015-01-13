using System.Collections;
using UnityEngine;

public class MissionMaster : Master {

	public static Mission currentMission { get; private set; }

	public MissionMaster() {
		//LoadMission();
	}

	~MissionMaster() {

	}

	public static void ReportMissionEvent(MissionEvent missionEvent) {
		currentMission.triggers[missionEvent].Invoke();
	}

	public void LoadMission() {
		currentMission = new MainMission();
		ReportMissionEvent(MissionEvent.Load);
	}
}

public enum MissionEvent {
	Load,
	ActorDeath,
	ActorSpawn
}
