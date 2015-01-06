using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankControllerPlayer : TankComponent
    {
        private void Update()
        {
            // Actions
            if (InputManager.GetAction(ActionCode.MoveForward)) {
                tank.MoveForward();
            }
            if (InputManager.GetAction(ActionCode.MoveBackward)) {
                tank.MoveBackward();
            }
            if (InputManager.GetAction(ActionCode.TurnLeft)) {
                tank.TurnLeft();
            }
            if (InputManager.GetAction(ActionCode.TurnRight)) {
                tank.TurnRight();
            }
            if (InputManager.GetAction(ActionCode.PrimaryFire)) {
                tank.Fire();
            }

            // Axes
            tank.LookHorizontal(InputManager.GetAxis(AxisCode.LookHorizontal));
            tank.LookVertical(InputManager.GetAxis(AxisCode.LookVertical));
        }
    }
}
