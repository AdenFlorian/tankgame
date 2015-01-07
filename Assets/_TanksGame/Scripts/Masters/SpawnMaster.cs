using System;
using System.Collections;
using UnityEngine;

public class SpawnMaster : Master
{
    public SpawnMaster()
    {
        Debug.Log(GetType().Name + " Loaded!");
        SpawnActor("Tank", ControllableBy.Player);
        SpawnActor("Tank", ControllableBy.AI, new Vector3(10f, 0f, 15f));
        SpawnActor("Tank", ControllableBy.None, new Vector3(-10f, 0f, 15f));
        SpawnActor("Tank", ControllableBy.AI, new Vector3(20f, 0f, 15f));
        SpawnActor("Tank", ControllableBy.None, new Vector3(-20f, 0f, 15f));
        SpawnActor("Tank", ControllableBy.AI, new Vector3(30f, 5f, -15f));
        SpawnActor("Tank", ControllableBy.AI, new Vector3(-40f, 0f, -15f));
    }

    public void SpawnActor(string actorClass,
        ControllableBy controller,
        Vector3 spawnPosition = new Vector3(),
        Quaternion spawnRotation = new Quaternion())
    {
        GameObject actorPrefab = Resources.Load<GameObject>(actorClass);
        GameObject spawnedActor = GameObject.Instantiate(actorPrefab, spawnPosition, spawnRotation) as GameObject;
        Actor newActor = spawnedActor.GetComponent<Actor>();
        newActor.OnSpawn();
        newActor.InitController(controller);
    }
}

public enum Actors
{
    Tank
}

public enum ControllableBy
{
    Player,
    AI,
    None
}
