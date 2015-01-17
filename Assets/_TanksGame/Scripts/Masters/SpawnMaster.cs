using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaster : Master
{

		private static Transform actorsParent;

		static Dictionary<string, Transform> actorParents = new Dictionary<string, Transform> ();

		public SpawnMaster ()
		{
				actorsParent = new GameObject ("Actors").transform;
				Debug.Log (GetType ().Name + " Loaded!");
		}

		public static T SpawnActor<T> (ControlledBy controller = ControlledBy.Empty,
		Vector3 spawnPos = new Vector3 (), Quaternion spawnRot = new Quaternion ()) where T : Actor
		{

				GameObject actorPrefab = Resources.Load<GameObject> (typeof(T).ToString ());
				GameObject spawnedActorGO = Instantiate (actorPrefab, spawnPos, spawnRot);
				T newActor = spawnedActorGO.GetComponent<T> ();
				newActor.OnSpawn ();
				newActor.InitController (controller);
				spawnedActorGO.name += newActor.actorID;
				return newActor;
		}

		public static T[] SpawnActors<T> (int count, ControlledBy controller = ControlledBy.Empty,
		Vector3[] spawnPoss = null, Quaternion[] spawnRots = null) where T : Actor
		{

				T[] newActors = new T[count];

				if (spawnPoss == null) {
						spawnPoss = new Vector3[] { new Vector3 () };
				}
				if (spawnRots == null) {
						spawnRots = new Quaternion[] { new Quaternion () };
				}
				for (int i = 0; i < count; i++) {
						var j = (int)(i - Mathf.Floor (i / spawnPoss.Length) * spawnPoss.Length);
						var k = (int)(i - Mathf.Floor (i / spawnRots.Length) * spawnRots.Length);
						newActors [i] = SpawnActor<T> (controller, spawnPoss [j], spawnRots [k]);
				}

				return newActors;
		}

		public static GameObject Instantiate (GameObject newGO)
		{
				return Instantiate (newGO, Vector3.zero, Quaternion.identity);
		}

		public static GameObject Instantiate (GameObject newGO, Vector3 position, Quaternion rotation)
		{
				GameObject spawnedGO = GameObject.Instantiate (newGO, position, rotation) as GameObject;
				SetActorParent (spawnedGO);
				return spawnedGO;
		}

		private static void SetActorParent (GameObject actorGO)
		{
				string actorName = actorGO.name;
				Transform parGO;
				if (actorParents.TryGetValue (actorName, out parGO) == false) {
						parGO = new GameObject (actorName + "s").transform;
						parGO.transform.parent = actorsParent;
						actorParents.Add (actorName, parGO);
				}
				actorGO.transform.parent = parGO;
		}

		public static void DespawnAllActors ()
		{
				GameObject.Destroy (actorsParent.gameObject);
				actorsParent = new GameObject ("Actors").transform;
				actorParents.Clear ();
		}
}

public enum ControlledBy
{
		PlayerLocal,
		AI,
		Empty,
		PlayerNetwork
}
