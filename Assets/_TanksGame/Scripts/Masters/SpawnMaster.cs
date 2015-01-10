using UnityEngine;

public class SpawnMaster : Master {
	public SpawnMaster() {
		Debug.Log(GetType().Name + " Loaded!");
		SpawnActor("Tank", ControlledBy.Player);
		SpawnActor("Tank", ControlledBy.AI, new Vector3(10f, 0f, 15f));
		SpawnActor("Tank", ControlledBy.Empty, new Vector3(-10f, 0f, 15f));
		SpawnActor("Tank", ControlledBy.AI, new Vector3(20f, 0f, 15f));
		SpawnActor("Tank", ControlledBy.Empty, new Vector3(-20f, 0f, 15f));
		SpawnActor("Tank", ControlledBy.AI, new Vector3(30f, 5f, -15f));
		SpawnActor("Tank", ControlledBy.AI, new Vector3(-40f, 0f, -15f));
	}

	public void SpawnActor(string actorClass, ControlledBy controller,
		Vector3 spawnPos = new Vector3(), Quaternion spawnRot = new Quaternion()) {

		GameObject actorPrefab = Resources.Load<GameObject>(actorClass);
		GameObject spawnedActor = GameObject.Instantiate(actorPrefab, spawnPos, spawnRot) as GameObject;
		Actor newActor = spawnedActor.GetComponent<Actor>();
		newActor.OnSpawn();
		newActor.InitController(controller);
	}
}

public enum ControlledBy {
	Player,
	AI,
	Empty
}
