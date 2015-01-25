using UnityEngine;

public class TankCamera : TankComponent {
	public bool follow = true;
	public bool lookAt = true;
	public Vector3 desiredPosition;

	public float desiredDistance = 2f;
	public float desiredHeightOffset = 0.3f;
	public float desiredAngleOffset = 7f;

	private GameObject cameraDolly;

	private void Start() {
		//cameraDolly = new GameObject("TankCameraDolly");
		cameraDolly = GameObject.Instantiate(Resources.Load<GameObject>("TankCamera")) as GameObject;
		//cameraDolly.AddComponent<Camera>();
		//cameraDolly.AddComponent<AudioListener>();
		cameraDolly.transform.parent = tank.transform;
	}

	private void LateUpdate() {
		if (tank != null) {
			if (follow) {
				Move();
			}
			if (lookAt) {
				cameraDolly.transform.LookAt(tank.tankTop.transform.position);
				cameraDolly.transform.Rotate(-desiredAngleOffset, 0f, 0f);
			}
		}
	}

	private void Move() {
		// Desired position equals the position vector
		// moved a certain distance
		// along the negative z local vector of the tank
		Vector3 backVector = tank.tankTop.transform.localToWorldMatrix
			.MultiplyVector(new Vector3(0f, desiredHeightOffset, -1f));
		Vector3 normBackVector = backVector.normalized;
		Vector3 multipliedBackVector = normBackVector * desiredDistance;
		desiredPosition = tank.tankTop.transform.position + multipliedBackVector;
		cameraDolly.transform.position = desiredPosition;
	}
}
