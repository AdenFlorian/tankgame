using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankAI : MonoBehaviour
    {
        private TankController tankController;

        public Transform nextWaypoint;
        public Vector3 distanceVector;
        public Vector3 worldSpaceForwardVector;
        public Vector3 crossProduct;
        public float distanceToWaypoint;
        public float dotProduct;

        public float angleTowardsWaypoint;

        public bool arrived = false;

        public bool pointedTowardsNextWaypoint = false;

        private void Awake()
        {
            tankController = GetComponent<TankController>();
        }

        private void Start()
        {
            nextWaypoint = GameMaster.playerTankController.transform;
        }

        private void Update()
        {
            distanceVector = nextWaypoint.position - tankController.transform.position;
            distanceToWaypoint = distanceVector.magnitude;
            worldSpaceForwardVector = tankController.transform.localToWorldMatrix.MultiplyVector(Vector3.forward);
            angleTowardsWaypoint = Vector3.Angle(distanceVector, worldSpaceForwardVector);
            dotProduct = Vector3.Dot(distanceVector, worldSpaceForwardVector);
            crossProduct = Vector3.Cross(distanceVector, worldSpaceForwardVector);
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
            if (crossProduct.y > 0) {
                tankController.TurnLeft();
            } else {
                tankController.TurnRight();
            }
        }
    }
}
