using UnityEngine;
using System.Collections;
namespace Tank
{
    public class TankAudio : MonoBehaviour
    {
        TankDriver tankDriver;

        void Awake()
        {
            tankDriver = GetComponent<TankDriver>();
        }

        void Start()
        {
        }

        void Update()
        {
            audio.pitch = tankDriver.speedNormalized;
        }
    }
}