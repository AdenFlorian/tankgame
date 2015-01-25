using UnityEngine;

public abstract class ActorController {

	protected Actor actor;

	public ActorController(Actor actor) {
		this.actor = actor;
		ControllerMaster.controllerUpdateActions.Add(ControllerUpdate);
	}

	protected virtual void ControllerUpdate() { }

	public void OnActorDeath() {
		ControllerMaster.controllerUpdateActions.Remove(ControllerUpdate);
	}
}
