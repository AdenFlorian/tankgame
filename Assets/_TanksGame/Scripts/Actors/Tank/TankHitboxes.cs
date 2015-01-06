using System.Collections;
using UnityEngine;

public class TankHitboxes : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Start()
    {
        //Get all hitboxes attached to tank
        Debug.Log(gameObject.GetComponentsInChildren<Collider>().Length);
    }

    private void Update()
    {
    }
}
