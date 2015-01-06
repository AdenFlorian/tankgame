using System.Collections;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int actorID { get; private set; }

    private static int nextID = 1;

    protected virtual void Awake()
    {
        actorID = Actor.nextID++;
    }
}
