using System.Collections;
using UnityEngine;

public class TankDeath : TankComponent {
	public GameObject deathFX;
	void Start() {
	}

	void Update() {
	}

	public void Die() {
		GameObject fx = GameObject.Instantiate(deathFX, tank.transform.position, tank.transform.rotation) as GameObject;
		Destroy(fx, 10);
		Destroy(tank.gameObject);
	}
}
