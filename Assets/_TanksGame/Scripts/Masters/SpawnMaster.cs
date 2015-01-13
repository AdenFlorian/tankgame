using System;
using UnityEngine;

public class SpawnMaster : Master {

	public SpawnMaster() {
		Debug.Log(GetType().Name + " Loaded!");
	}

	public static void SpawnActor(Type actorType, ControlledBy controller = ControlledBy.Empty,
		Vector3 spawnPos = new Vector3(), Quaternion spawnRot = new Quaternion()) {

		GameObject actorPrefab = Resources.Load<GameObject>(actorType.ToString());
		GameObject spawnedActor = GameObject.Instantiate(actorPrefab, spawnPos, spawnRot) as GameObject;
		Actor newActor = spawnedActor.GetComponent<Actor>();
		newActor.OnSpawn();
		newActor.InitController(controller);
	}

	public static void SpawnActors(Type actorType, int count, ControlledBy controller = ControlledBy.Empty,
		Vector3[] spawnPoss = null, Quaternion[] spawnRots = null) {

		if (spawnPoss == null) {
			spawnPoss = new Vector3[] { new Vector3() };
		}
		if (spawnRots == null) {
			spawnRots = new Quaternion[] { new Quaternion() };
		}
		for (int i = 0; i < count; i++) {
			var j = (int)(i - Mathf.Floor(i / spawnPoss.Length) * spawnPoss.Length);
			var k = (int)(i - Mathf.Floor(i / spawnRots.Length) * spawnRots.Length);
			SpawnActor(actorType, controller, spawnPoss[j], spawnRots[k]);
		}
	}
}

public enum ControlledBy {
	Player,
	AI,
	Empty
}
