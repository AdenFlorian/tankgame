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
}
