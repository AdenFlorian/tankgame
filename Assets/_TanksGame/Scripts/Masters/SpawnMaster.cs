using System;
using UnityEngine;

public class SpawnMaster : Master {

	public SpawnMaster() {
		Debug.Log(GetType().Name + " Loaded!");
	}

	public static Actor SpawnActor(Type actorType, ControlledBy controller = ControlledBy.Empty,
		Vector3 spawnPos = new Vector3(), Quaternion spawnRot = new Quaternion()) {

		GameObject actorPrefab = Resources.Load<GameObject>(actorType.ToString());
		GameObject spawnedActor = GameObject.Instantiate(actorPrefab, spawnPos, spawnRot) as GameObject;
		Actor newActor = spawnedActor.GetComponent<Actor>();
		newActor.OnSpawn();
		newActor.InitController(controller);
		return newActor;
	}

	public static Actor[] SpawnActors(Type actorType, int count, ControlledBy controller = ControlledBy.Empty,
		Vector3[] spawnPoss = null, Quaternion[] spawnRots = null) {

		Actor[] newActors = new Actor[count];

		if (spawnPoss == null) {
			spawnPoss = new Vector3[] { new Vector3() };
		}
		if (spawnRots == null) {
			spawnRots = new Quaternion[] { new Quaternion() };
		}
		for (int i = 0; i < count; i++) {
			var j = (int)(i - Mathf.Floor(i / spawnPoss.Length) * spawnPoss.Length);
			var k = (int)(i - Mathf.Floor(i / spawnRots.Length) * spawnRots.Length);
			newActors[i] = SpawnActor(actorType, controller, spawnPoss[j], spawnRots[k]);
		}

		return newActors;
	}

	public static T SpawnActor<T>(ControlledBy controller = ControlledBy.Empty,
		Vector3 spawnPos = new Vector3(), Quaternion spawnRot = new Quaternion()) where T : Actor {

		GameObject actorPrefab = Resources.Load<GameObject>(typeof(T).ToString());
		GameObject spawnedActor = GameObject.Instantiate(actorPrefab, spawnPos, spawnRot) as GameObject;
		T newActor = spawnedActor.GetComponent<T>();
		newActor.OnSpawn();
		newActor.InitController(controller);
		return newActor;
	}

	public static T[] SpawnActors<T>(int count, ControlledBy controller = ControlledBy.Empty,
		Vector3[] spawnPoss = null, Quaternion[] spawnRots = null) where T : Actor {

		T[] newActors = new T[count];

		if (spawnPoss == null) {
			spawnPoss = new Vector3[] { new Vector3() };
		}
		if (spawnRots == null) {
			spawnRots = new Quaternion[] { new Quaternion() };
		}
		for (int i = 0; i < count; i++) {
			var j = (int)(i - Mathf.Floor(i / spawnPoss.Length) * spawnPoss.Length);
			var k = (int)(i - Mathf.Floor(i / spawnRots.Length) * spawnRots.Length);
			newActors[i] = SpawnActor<T>(controller, spawnPoss[j], spawnRots[k]);
		}

		return newActors;
	}
}

public enum ControlledBy {
	PlayerLocal,
	AI,
	Empty,
	PlayerNetwork
}
