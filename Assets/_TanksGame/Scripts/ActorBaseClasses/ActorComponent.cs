using UnityEngine;

public abstract class ActorComponent : MonoBehaviour {
	protected Actor actor;

	protected virtual void Awake() {
		FindParentModel();
	}

	protected void FindParentModel() {
		actor = GetComponent<Actor>();
		if (actor == null) {
			actor = GetComponentInParent<Actor>();
			if (actor == null) {
				throw new ComponentWithoutParentModelException();
			}
		}
	}

	public virtual void OnActorDeath() { }
}

[System.Serializable]
public class ComponentWithoutParentModelException : System.Exception {
	public ComponentWithoutParentModelException() {
	}

	public ComponentWithoutParentModelException(string message)
		: base(message) {
	}

	public ComponentWithoutParentModelException(string message, System.Exception inner)
		: base(message, inner) {
	}

	protected ComponentWithoutParentModelException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context)
		: base(info, context) {
	}
}
