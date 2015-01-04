using System.Collections;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    public Tank.TankController tankController;
    public Transform nextWaypoint;
    public Vector3 distanceVector;
    public Vector3 worldSpaceForwardVector;
    public float distanceToWaypoint;

    public float angleTowardsWaypoint;

    public bool arrived = false;

    public bool pointedTowardsNextWaypoint = false;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
        distanceVector = nextWaypoint.position - tankController.transform.position;
        distanceToWaypoint = distanceVector.magnitude;
        worldSpaceForwardVector = tankController.transform.localToWorldMatrix.MultiplyVector(Vector3.forward);
        angleTowardsWaypoint = Vector3.Angle(distanceVector, worldSpaceForwardVector);

        CheckIfArrived();

        if (arrived) {
        } else {
            DriveTowardsNextWaypoint();
        }
    }

    private void CheckIfArrived()
    {
        if (distanceToWaypoint < 5) {
            arrived = true;
        } else {
            arrived = false;
        }
    }

    private void DriveTowardsNextWaypoint()
    {
        // Check if pointed towards next waypoint
        CheckOrientationTowardsWaypoint();
        if (pointedTowardsNextWaypoint) {
            tankController.MoveForward();
        } else {
            // Point self towards next waypnt
            OrientToWaypoint();
        }
    }

    private void CheckOrientationTowardsWaypoint()
    {
        if (angleTowardsWaypoint < 15) {
            pointedTowardsNextWaypoint = true;
        } else {
            pointedTowardsNextWaypoint = false;
        }
    }

    private void OrientToWaypoint()
    {
        tankController.TurnLeft();
    }
}
