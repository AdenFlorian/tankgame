using UnityEngine;

public enum LeftRight {
	Left,
	Right
}

public enum UpDown {
	Down,
	Up
}

public class TankMover : TankComponent {
	// Max values
	//[Range(0, 1)]
	public float maxForward = 5f;
	//[Range(0, 0.5f)]
	public float maxBackward = 2.5f;
	//[Range(0, 4)]
	public float maxTurnRate = 100f;

	// Acceleration values
	public float accelForward = 5f;
	public float accelBackward = 2.5f;
	public float accelTurn = 200f;

	// Friction Values
	public float frictionTurn = 300f;
	public float frictionforward = 4.5f;

	// Current values
	public float currentForwardSpeed = 0f;
	public float currentTurnRate = 0f;
	public float currentRPM = 0f;

	float forthBackForce = 0f;

	public float speedNormalized {
		get {
			return currentForwardSpeed / maxForward;
		}
		private set {
			speedNormalized = value;
		}
	}

	// Main gun movement (look)
	public float maxGunAngle;
	public float minGunAngle;
	private float gunAngle = 0f;

	bool isGrounded = false;

	private void Update() {
	}

	private void FixedUpdate() {
		TankMove tankMove = tank.tankMove;
		RotateGun(tankMove.rotateGun);
		AngleGun(tank.mainGun.transform, tankMove.angleGun);
		MoveOrder(tankMove);

		if (currentTurnRate != 0f) {
			transform.Rotate(0f, currentTurnRate * Time.deltaTime, 0f, Space.Self);
		}

		CheckIfGrounded();

		Vector3 moveVec3 = Vector3.forward * forthBackForce * Time.deltaTime * 1000;

		if (isGrounded && moveVec3.magnitude != 0f) {
			rigidbody.AddRelativeForce(moveVec3, ForceMode.Acceleration);
		}

		tank.tankMove.Clear();
	}

	private void CheckIfGrounded() {
		Vector3 tankDownVecWorld = transform.localToWorldMatrix.MultiplyVector(-Vector3.up);
		Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0f), tankDownVecWorld);
		float rayLength = 0.2f;
		Debug.DrawRay(ray.origin, ray.direction * rayLength);
		bool hitSomething = Physics.Raycast(ray, rayLength);
		if (hitSomething) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
	}

	// Rotate gun on y axis left or right
	private void RotateGun(float degrees, LeftRight leftOrRight) {
		// Positive to right or clockwise
		// Negative to the left or counterclockwise
		if (leftOrRight == LeftRight.Left) {
			degrees = -degrees;
		}
		RotateGun(degrees);
	}

	// Rotate gun on y axis left or right
	private void RotateGun(float degrees) {
		tank.tankTop.transform.Rotate(0f, degrees, 0f, Space.Self);
	}

	private void AngleGun(Transform gunTrans, float degrees, UpDown upOrDown) {

		if (upOrDown == UpDown.Down) {
			degrees = -degrees;
		}

		AngleGun(gunTrans, degrees);
	}

	// Should only rotate gun once, clamp degrees before rotating
	private void AngleGun(Transform gunTrans, float degrees) {

		float desiredAngle = gunAngle + degrees;
		float clampedAngle = Mathf.Clamp(desiredAngle, minGunAngle, maxGunAngle);

		gunTrans.Rotate(clampedAngle - gunAngle, 0f, 0f, Space.Self);

		gunAngle = clampedAngle;
	}

	private void MoveOrder(TankMove moveOrder) {
		if (moveOrder.forth) {
			currentRPM += accelForward * Time.deltaTime;
			forthBackForce = accelForward * Mathf.Clamp((-transform.worldToLocalMatrix.MultiplyVector(rigidbody.velocity).z / maxForward) + 1, 0, 1);
		} else if (moveOrder.back) {
			currentRPM -= accelBackward * Time.deltaTime;
			forthBackForce = -accelForward * Mathf.Clamp((-Mathf.Abs(transform.worldToLocalMatrix.MultiplyVector(rigidbody.velocity).z) / maxForward) + 1, 0, 1);
		} else {

			forthBackForce = 0f;
			if (currentForwardSpeed > 0) {
				currentForwardSpeed -= frictionforward * Time.deltaTime;
				currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, 0, maxForward);
			} else if (currentForwardSpeed < 0) {
				currentForwardSpeed += frictionforward * Time.deltaTime;
				currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, -maxBackward, 0);
			}
		}

		if (moveOrder.left) {
			currentTurnRate -= accelTurn * Time.deltaTime;
		} else if (moveOrder.right) {
			currentTurnRate += accelTurn * Time.deltaTime;
		} else {
			if (currentTurnRate > 0) {
				currentTurnRate -= frictionTurn * Time.deltaTime;
				currentTurnRate = Mathf.Clamp(currentTurnRate, 0, maxTurnRate);
			} else if (currentTurnRate < 0) {
				currentTurnRate += frictionTurn * Time.deltaTime;
				currentTurnRate = Mathf.Clamp(currentTurnRate, -maxTurnRate, 0);
			}
		}

		currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, -maxBackward, maxForward);
		currentTurnRate = Mathf.Clamp(currentTurnRate, -maxTurnRate, maxTurnRate);
	}
}
