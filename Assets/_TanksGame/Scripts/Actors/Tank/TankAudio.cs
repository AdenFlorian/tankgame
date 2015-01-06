using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankAudio : TankComponent
    {
        protected void Update()
        {
            audio.pitch = tank.mover.speedNormalized;
        }
    }
}
