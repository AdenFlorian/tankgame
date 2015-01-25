using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mission {

	public Dictionary<MissionEvent, Action> triggers = new Dictionary<MissionEvent, Action>();

	public Mission() {
		foreach (MissionEvent missionEvent in Enum.GetValues(typeof(MissionEvent))) {
			triggers.Add(missionEvent, null);
		}
	}
}
