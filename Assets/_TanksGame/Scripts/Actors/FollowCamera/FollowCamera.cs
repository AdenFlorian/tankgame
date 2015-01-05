﻿using System.Collections;
using UnityEngine;

public class FollowCamera : ActorModel
{
    public Transform target;
    public bool follow = true;
    public bool lookAt = true;
    public Vector3 desiredPosition;

    public float desiredDistance = 10f;
    public float desiredHeightOffset = 10f;
    public float desiredAngleOffset = 10f;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    private void LateUpdate()
    {
        if (follow) {
            Follow();
        }
        if (lookAt) {
            transform.LookAt(target.position);
            transform.Rotate(-desiredAngleOffset, 0f, 0f);
        }
    }

    private void Follow()
    {
        // Desired position equals the position vector
        // moved a certain distance
        // along the negative z local vector of the target
        Vector3 backVector = target.localToWorldMatrix
            .MultiplyVector(new Vector3(0f, desiredHeightOffset, -1f));
        Vector3 normBackVector = backVector.normalized;
        Vector3 multipliedBackVector = normBackVector * desiredDistance;
        desiredPosition = target.position + multipliedBackVector;
        transform.position = desiredPosition;
    }
}