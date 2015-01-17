using System.Collections;
using UnityEngine;

public class TankDeath : TankComponent {

	public GameObject deathFX;

	void Start() {
	}

	void Update() {
	}

	public void Explode() {
		GameObject fx = SpawnMaster.Instantiate(deathFX, tank.transform.position, tank.transform.rotation);
		Destroy(fx, 10);
	}
}
