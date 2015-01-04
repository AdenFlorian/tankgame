using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankAudio : MonoBehaviour
    {
        private TankModel tankDriver;

        private void Awake()
        {
            tankDriver = GetComponent<TankModel>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            audio.pitch = tankDriver.speedNormalized;
        }
    }
}
