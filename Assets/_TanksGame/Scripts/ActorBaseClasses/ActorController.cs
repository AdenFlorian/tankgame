using UnityEngine;

public abstract class ActorController {

	protected Actor actor;

	public ActorController(Actor actor) {
		this.actor = actor;
		Master.controllerMaster.controllerUpdateActions.Add(ControllerUpdate);
	}

	protected virtual void ControllerUpdate() { }

	public void OnActorDeath() {
		Master.controllerMaster.controllerUpdateActions.Remove(ControllerUpdate);
	}
}
