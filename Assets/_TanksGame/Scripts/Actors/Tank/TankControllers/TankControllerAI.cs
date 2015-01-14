using UnityEngine;

public class TankControllerAI : TankController {

	public Vector3 distanceVector;
	public Vector3 worldSpaceForwardVector;
	public Vector3 crossProduct;
	public float distanceToWaypoint;
	public float dotProduct;
	public float angleTowardsWaypoint;	// Degree of difference between forward vector and vector towards waypoint

	public bool arrived = false;
	public bool pointedTowardsNextWaypoint = false;

	public TankControllerAI(Tank tank)
		: base(tank) {
		nextWaypoint = GameObject.FindGameObjectWithTag("playerTank").transform;
	}

	protected override void ControllerUpdate() {

		DoMathStuff();
		CheckIfArrived();

		switch (state) {
			case TankMoveOrders.FollowTarget:
				OrientToWaypoint();
				if (!arrived) DriveTowardsNextWaypoint();
				break;
			case TankMoveOrders.Hold:
				OrientToWaypoint();
				break;
			case TankMoveOrders.SpinLeft:
				tank.TurnLeft();
				break;
			case TankMoveOrders.SpinRight:
				tank.TurnRight();
				break;
			default:
				break;
		}
	}

	private void DoMathStuff() {
		distanceVector = nextWaypoint.position - tank.transform.position;
		distanceToWaypoint = distanceVector.magnitude;
		worldSpaceForwardVector = tank.transform.localToWorldMatrix.MultiplyVector(Vector3.forward);
		angleTowardsWaypoint = Vector3.Angle(distanceVector, worldSpaceForwardVector);
		dotProduct = Vector3.Dot(distanceVector, worldSpaceForwardVector);
		crossProduct = Vector3.Cross(distanceVector, worldSpaceForwardVector);
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
			tank.MoveForward();
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
