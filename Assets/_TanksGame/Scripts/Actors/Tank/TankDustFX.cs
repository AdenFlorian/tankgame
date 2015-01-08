using UnityEngine;

public class TankDustFX : TankComponent {
	private float startEmissionRate;

	protected void Start() {
		startEmissionRate = particleSystem.emissionRate;
	}

	protected void Update() {
		particleSystem.emissionRate = startEmissionRate * Mathf.Abs(tank.mover.speedNormalized);
	}
}
