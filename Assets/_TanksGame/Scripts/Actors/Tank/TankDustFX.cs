using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankDustFX : MonoBehaviour
    {
        private Tank tankModel;
        private float startEmissionRate;

        private void Awake()
        {
            tankModel = GetComponentInParent<Tank>();
            startEmissionRate = particleSystem.emissionRate;
        }

        private void Start()
        {
        }

        private void Update()
        {
            particleSystem.emissionRate = startEmissionRate * Mathf.Abs(tankModel.speedNormalized);
        }
    }
}
