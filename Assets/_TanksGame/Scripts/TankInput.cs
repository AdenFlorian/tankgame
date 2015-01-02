using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankInput : MonoBehaviour
    {
        public TankCommander tankCommander;

        private void Awake()
        {
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.W)) {
                tankCommander.moveForward();
            }
            if (Input.GetKey(KeyCode.S)) {
                tankCommander.moveBackward();
            }
            if (Input.GetKey(KeyCode.A)) {
                tankCommander.turnLeft();
            }
            if (Input.GetKey(KeyCode.D)) {
                tankCommander.turnRight();
            }
            if (Input.GetAxis("Mouse X") > 0f) {
            }
        }
    }
}