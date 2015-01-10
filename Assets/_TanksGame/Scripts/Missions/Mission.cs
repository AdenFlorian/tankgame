using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mission {

	public Dictionary<MissionEvent, List<Action>> triggers = new Dictionary<MissionEvent, List<Action>>();
}
