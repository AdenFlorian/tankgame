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
                tankCommander.MoveForward();
            }
            if (Input.GetKey(KeyCode.S)) {
                tankCommander.MoveBackward();
            }
            if (Input.GetKey(KeyCode.A)) {
                tankCommander.TurnLeft();
            }
            if (Input.GetKey(KeyCode.D)) {
                tankCommander.TurnRight();
            }

            if (Master.inputMaster.mouse0) {
                tankCommander.Fire();
            }

            tankCommander.LookHorizontal(Input.GetAxis("Mouse X"));
            tankCommander.LookVertical(Input.GetAxis("Mouse Y"));
        }
    }
}