using UnityEngine;

public class FirstScript : MonoBehaviour {
	private void Awake() {
		Master.MasterAwake();
	}

	private void Start() {
	}

	private void Update() {
		Master.StartMasterUpdate();
	}
}
