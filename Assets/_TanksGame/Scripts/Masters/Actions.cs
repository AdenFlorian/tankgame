using UnityEngine;
using System.Collections;
using System;

public static class Actions
{
    public static Action TankMoveForwardAction =
        () => GameMaster.playerTank.MoveForward();

    public static Action TankMoveBackwardAction =
        () => GameMaster.playerTank.MoveBackward();

    public static Action TankTurnLeftAction =
        () => GameMaster.playerTank.TurnLeft();

    public static Action TankTurnRightAction =
        () => GameMaster.playerTank.TurnRight();

    public static Action TankFireAction =
        () => GameMaster.playerTank.Fire();

    public static Action TankLookHorizontalAction =
        () => GameMaster.playerTank.LookHorizontal(Input.GetAxis("Mouse X"));

    public static Action TankLookVerticalAction =
        () => GameMaster.playerTank.LookVertical(Input.GetAxis("Mouse Y"));
}
