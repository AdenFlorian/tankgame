using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankAudio : TankComponent
    {
        private void Update()
        {
            audio.pitch = tank.mover.speedNormalized;
        }
    }
}
