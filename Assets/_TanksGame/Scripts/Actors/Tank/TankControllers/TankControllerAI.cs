using System.Collections;
using UnityEngine;

public class TankControllerAI : TankController {

	public Vector3 distanceVector;
	public Vector3 worldSpaceForwardVector;
	public Vector3 worldSpaceForwardGunVector;
	public Vector3 crossFromTankToWaypnt;
	public Vector3 crossFromGunToWaypnt;
	public float distanceToWaypoint;
	public float dotProduct;
	public float angleTowardsWaypoint;	// Degree of difference between forward vector and vector towards waypoint
	public float angleFromGunToWaypoint;

	public bool arrived = false;
	public bool pointedTowardsNextWaypoint = false;

	public TankControllerAI(Tank tank)
		: base(tank) {
		nextWaypoint = GameObject.FindGameObjectWithTag("playerTank").transform;
	}

	protected override void ControllerUpdate() {

		if (nextWaypoint != null) {
			CalculateWaypntStuff();
		}

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

		AimCannonAtTarget();

		tank.Fire();
	}

	private void CalculateWaypntStuff() {
		distanceVector = nextWaypoint.position - tank.transform.position;
		distanceToWaypoint = distanceVector.magnitude;
		worldSpaceForwardVector = tank.transform.localToWorldMatrix.MultiplyVector(Vector3.forward);
		worldSpaceForwardGunVector = tank.tankTop.transform.localToWorldMatrix.MultiplyVector(Vector3.forward);
		angleTowardsWaypoint = Vector3.Angle(distanceVector, worldSpaceForwardVector);
		angleFromGunToWaypoint = Vector3.Angle(distanceVector, worldSpaceForwardGunVector);
		dotProduct = Vector3.Dot(distanceVector, worldSpaceForwardVector);
		crossFromTankToWaypnt = Vector3.Cross(distanceVector, worldSpaceForwardVector);
		crossFromGunToWaypnt = Vector3.Cross(distanceVector, worldSpaceForwardGunVector);
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
		if (crossFromTankToWaypnt.y > 0) {
			tank.TurnLeft();
		} else {
			tank.TurnRight();
		}
	}

	private void AimCannonAtTarget() {
		float angleToTarget = angleFromGunToWaypoint;
		if (crossFromGunToWaypnt.y > 0) {
			angleToTarget = -angleToTarget;
		}
		tank.LookHorizontal(angleToTarget + Random.Range(-30.5f, 35.5f));
	}
}
