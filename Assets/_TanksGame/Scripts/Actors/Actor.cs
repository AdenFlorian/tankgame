using System.Collections;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    protected bool isSpawned = false;
    public int actorID { get; private set; }

    private static int nextID = 1;

    public void OnSpawn()
    {
        actorID = Actor.nextID++;
        isSpawned = true;
    }

    public virtual void InitController(ControllableBy controllerType)
    {
    }
}
