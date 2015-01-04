using UnityEngine;
using System.Collections;
namespace Tank
{
    public class TankAudio : MonoBehaviour
    {
        TankModel tankDriver;

        void Awake()
        {
            tankDriver = GetComponent<TankModel>();
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