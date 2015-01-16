using System;
using UnityEngine;

public class SpawnMaster : Master {

	public SpawnMaster() {
		Debug.Log(GetType().Name + " Loaded!");
	}

	public static T SpawnActor<T>(ControlledBy controller = ControlledBy.Empty,
		Vector3 spawnPos = new Vector3(), Quaternion spawnRot = new Quaternion()) where T : Actor {

		GameObject actorPrefab = Resources.Load<GameObject>(typeof(T).ToString());
		GameObject spawnedActor = GameObject.Instantiate(actorPrefab, spawnPos, spawnRot) as GameObject;
		T newActor = spawnedActor.GetComponent<T>();
		newActor.OnSpawn();
		newActor.InitController(controller);
		spawnedActor.name += spawnedActor.GetComponent<Actor>().actorID;
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
