using UnityEngine;

public class TankControllerAI : TankController {
	public Transform nextWaypoint;
	public Vector3 distanceVector;
	public Vector3 worldSpaceForwardVector;
	public Vector3 crossProduct;
	public float distanceToWaypoint;
	public float dotProduct;

	public float angleTowardsWaypoint;

	public bool arrived = false;

	public bool pointedTowardsNextWaypoint = false;

	public TankControllerAI(Tank tank)
		: base(tank) {
		nextWaypoint = GameObject.FindGameObjectWithTag("playerTank").transform;
	}

	protected override void ControllerUpdate() {
		distanceVector = nextWaypoint.position - tank.transform.position;
		distanceToWaypoint = distanceVector.magnitude;
		worldSpaceForwardVector = tank.transform.localToWorldMatrix.MultiplyVector(Vector3.forward);
		angleTowardsWaypoint = Vector3.Angle(distanceVector, worldSpaceForwardVector);
		dotProduct = Vector3.Dot(distanceVector, worldSpaceForwardVector);
		crossProduct = Vector3.Cross(distanceVector, worldSpaceForwardVector);
		CheckIfArrived();

		if (arrived) {
		} else {
			DriveTowardsNextWaypoint();
		}
	}

	private void CheckIfArrived() {
		if (distanceToWaypoint < 5) {
			arrived = true;
		} else {
			arrived = false;
		}
	}

	private void DriveTowardsNextWaypoint() {
		// Check if pointed towards next waypoint
		CheckOrientationTowardsWaypoint();
		if (pointedTowardsNextWaypoint) {
			tank.MoveForward();
		} else {
			// Point self towards next waypnt
			OrientToWaypoint();
		}
	}

	private void CheckOrientationTowardsWaypoint() {
		if (angleTowardsWaypoint < 15) {
			pointedTowardsNextWaypoint = true;
		} else {
			pointedTowardsNextWaypoint = false;
		}
	}

	private void OrientToWaypoint() {
		if (crossProduct.y > 0) {
			tank.TurnLeft();
		} else {
			tank.TurnRight();
		}
	}
}
