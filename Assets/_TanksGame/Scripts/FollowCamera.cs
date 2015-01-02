using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    public bool follow = true;
    public Vector3 desiredPosition;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (follow) {
            Follow();
        }
    }

    private void Follow()
    {
        float x, y, z;

        var targetPosition = target.transform.position;
    }
}