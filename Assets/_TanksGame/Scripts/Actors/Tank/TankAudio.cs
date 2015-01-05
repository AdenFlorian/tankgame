using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankAudio : TankComponent
    {
        protected override void Awake()
        {
            base.Awake();

            // do stuff
        }

        private void Start()
        {
        }

        private void Update()
        {
            audio.pitch = tankModel.speedNormalized;
        }
    }
}
