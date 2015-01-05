using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankDustFX : MonoBehaviour
    {
        private TankModel tankModel;
        private float startEmissionRate;

        private void Awake()
        {
            tankModel = GetComponentInParent<TankModel>();
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
