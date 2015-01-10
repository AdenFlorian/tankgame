using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Master {
	public class MissionMaster : Master {

		public Mission currentMission { get; private set; }

		public Dictionary<MissionEvent, Action> triggers;

		public MissionMaster() {
			LoadMission();
		}

		~MissionMaster() {

		}

		public void ReportActorEvent(Actor actor, MissionEvent missionEvent) {
			foreach (Action reaction in currentMission.triggers[missionEvent]) {
				reaction.Invoke();
			}
		}

		public void LoadMission() {
			currentMission = new MainMission();
		}
	}
}

public enum MissionEvent {
	ActorDeath,
	ActorSpawn
}
