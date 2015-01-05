using UnityEngine;
using System.Collections;
using System;

public static class Actions
{
    public static Action TankMoveForwardAction =
        () => GameMaster.playerTankController.MoveForward();

    public static Action TankMoveBackwardAction =
        () => GameMaster.playerTankController.MoveBackward();

    public static Action TankTurnLeftAction =
        () => GameMaster.playerTankController.TurnLeft();

    public static Action TankTurnRightAction =
        () => GameMaster.playerTankController.TurnRight();

    public static Action TankFireAction =
        () => GameMaster.playerTankController.Fire();

    public static Action TankLookHorizontalAction =
        () => GameMaster.playerTankController.LookHorizontal(Input.GetAxis("Mouse X"));

    public static Action TankLookVerticalAction =
        () => GameMaster.playerTankController.LookVertical(Input.GetAxis("Mouse Y"));
}
