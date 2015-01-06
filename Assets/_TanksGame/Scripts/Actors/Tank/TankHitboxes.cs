using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankHitboxes : TankComponent
    {
        private void Start()
        {
            //Get all hitboxes attached to tank
            Debug.Log(gameObject.GetComponentsInChildren<Collider>().Length);
        }

        private void Update()
        {
        }
    }
}
