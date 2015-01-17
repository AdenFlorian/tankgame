using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : Actor {

	public static AmmoPool Instance;

	public GameObject tankShellPrefab;

	private int initalPoolSize = 100;
	private int regenerateAmount = 20;

	private Queue<GameObject> tankShellQueue;

	private void Awake() {
		tankShellQueue = new Queue<GameObject>(initalPoolSize);
		Instance = this;
		GenerateMoreAmmo(initalPoolSize);
	}

	private void Start() {
	}

	private void Update() {
	}

	public GameObject GetNextShell() {
		if (tankShellQueue.Count == 0) {
			GenerateMoreAmmo(regenerateAmount);
		}
		return tankShellQueue.Dequeue();
	}

	public void ReturnShell(GameObject retrunedGO) {
		tankShellQueue.Enqueue(retrunedGO);
	}

	private void GenerateMoreAmmo(int amount) {
		GameObject newShell;

		for (int i = 0; i < amount; i++) {
			newShell = SpawnMaster.Instantiate(tankShellPrefab);
			tankShellQueue.Enqueue(newShell);
			newShell.SetActive(false);
			newShell.rigidbody.isKinematic = true;
		}
	}
}
