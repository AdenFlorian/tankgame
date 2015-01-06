using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankDustFX : TankComponent
    {
        private float startEmissionRate;

        private void Start()
        {
            startEmissionRate = particleSystem.emissionRate;
        }

        private void Update()
        {
            particleSystem.emissionRate = startEmissionRate * Mathf.Abs(tank.mover.speedNormalized);
        }
    }
}
