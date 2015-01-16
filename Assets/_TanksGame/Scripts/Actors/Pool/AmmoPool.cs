using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour {
	public static AmmoPool Instance;

	public GameObject tankShellPrefab;

	public int poolSize = 1000;

	private Queue<GameObject> tankShellQueue;

	private void Awake() {
		tankShellQueue = new Queue<GameObject>(poolSize);
		Instance = this;

		GameObject newShell;

		for (int i = 0; i < poolSize; i++) {
			newShell = GameObject.Instantiate(tankShellPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tankShellQueue.Enqueue(newShell);
			newShell.SetActive(false);
			newShell.rigidbody.isKinematic = true;
			newShell.transform.parent = transform;
		}
	}

	private void Start() {
	}

	private void Update() {
	}

	public GameObject GetNextShell() {
		return tankShellQueue.Dequeue();
	}

	public void ReturnShell(GameObject retrunedGO) {
		tankShellQueue.Enqueue(retrunedGO);
	}
}
