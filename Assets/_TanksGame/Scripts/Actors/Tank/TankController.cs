using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankController : ActorController
    {
        protected Tank tank;

        protected virtual void Awake()
        {
            tank = GetComponent<Tank>();
            if (tank == null) {
                Debug.LogError("TankController without a Tank!");
            }
        }
    }
}
