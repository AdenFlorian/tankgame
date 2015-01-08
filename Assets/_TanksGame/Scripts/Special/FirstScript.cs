using UnityEngine;

public class FirstScript : MonoBehaviour {
	private void Awake() {
		Master.Begin();
	}

	private void Start() {
	}

	private void Update() {
		Master.gameMaster.Update();
	}
}
