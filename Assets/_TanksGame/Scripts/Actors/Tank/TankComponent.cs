using System.Collections;
using UnityEngine;

namespace Tank
{
    public abstract class TankComponent : ActorComponent
    {
        protected Tank tankModel;

        protected override void Awake()
        {
            base.Awake();
            tankModel = model as Tank;
        }
    }
}
