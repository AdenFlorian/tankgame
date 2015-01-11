using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMaster : Master {

	public List<Action> controllerUpdateActions = new List<Action>();

	public ControllerMaster() {

	}

	~ControllerMaster() {

	}

	protected override void MasterUpdate() {
		foreach (Action controllerUpdateAction in controllerUpdateActions) {
			controllerUpdateAction.Invoke();
		}
	}
}
