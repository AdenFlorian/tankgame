using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

	public Transform target;

	public float revolutionTime = 1f;
	public float distance = 100f;
	public float angleAdjust = 10f;

	void Awake() {
	}

	void Start() {
	}

	void Update() {
		//transform.Rotate(Vector3.up, (360f * Time.deltaTime) / revolutionTime, Space.World);
		float newTime = Time.time / revolutionTime;
		transform.position = (new Vector3(Mathf.Sin(newTime) * distance, transform.position.y, Mathf.Cos(newTime) * distance)) + target.position;
		transform.LookAt(target.position);
		transform.Rotate(-angleAdjust, 0f, 0f, Space.Self);
	}
}
