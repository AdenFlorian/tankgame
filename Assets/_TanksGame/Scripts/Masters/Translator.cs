using System.Collections;
using UnityEngine;

public class Translator : MonoBehaviour
{
    private Command KeyHoldW = new TankMoveForwardCommand();
    private Command KeyHoldS = new TankMoveBackwardCommand();
    private Command KeyHoldA = new TankTurnLeftCommand();
    private Command KeyHoldD = new TankTurnRightCommand();
    private Command MouseDown0 = new TankFireCommand();
    private Command MouseXAxis = new TankLookHorizontalCommand();
    private Command MouseYAxis = new TankLookVerticalCommand();

    private void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && KeyHoldW != null) {
            KeyHoldW.Execute(GameMaster.playerTankController);
        }
        if (Input.GetKey(KeyCode.S) && KeyHoldS != null) {
            KeyHoldS.Execute(GameMaster.playerTankController);
        }
        if (Input.GetKey(KeyCode.A) && KeyHoldA != null) {
            KeyHoldA.Execute(GameMaster.playerTankController);
        }
        if (Input.GetKey(KeyCode.D) && KeyHoldD != null) {
            KeyHoldD.Execute(GameMaster.playerTankController);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && MouseDown0 != null) {
            MouseDown0.Execute(GameMaster.playerTankController);
        }

        // Axes (Ranges)
        if (MouseXAxis != null) {
            MouseXAxis.Execute(GameMaster.playerTankController);
        }
        if (MouseYAxis != null) {
            MouseYAxis.Execute(GameMaster.playerTankController);
        }
    }
}

public abstract class Command
{
    public abstract void Execute(Controller controller);
}

public class TankMoveForwardCommand : Command
{
    public override void Execute(Controller tankControl)
    {
        ((Tank.TankController)tankControl).MoveForward();
    }
}

public class TankMoveBackwardCommand : Command
{
    public override void Execute(Controller tankControl)
    {
        ((Tank.TankController)tankControl).MoveBackward();
    }
}

public class TankTurnLeftCommand : Command
{
    public override void Execute(Controller tankControl)
    {
        ((Tank.TankController)tankControl).TurnLeft();
    }
}

public class TankTurnRightCommand : Command
{
    public override void Execute(Controller tankControl)
    {
        ((Tank.TankController)tankControl).TurnRight();
    }
}

public class TankFireCommand : Command
{
    public override void Execute(Controller tankControl)
    {
        ((Tank.TankController)tankControl).Fire();
    }
}

public class TankLookHorizontalCommand : Command
{
    public override void Execute(Controller tankControl)
    {
        ((Tank.TankController)tankControl).LookHorizontal(Input.GetAxis("Mouse X"));
    }
}

public class TankLookVerticalCommand : Command
{
    public override void Execute(Controller tankControl)
    {
        ((Tank.TankController)tankControl).LookVertical(Input.GetAxis("Mouse Y"));
    }
}
