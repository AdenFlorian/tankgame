
public class TankControllerPlayer : TankController {

	public TankControllerPlayer(Tank tank)
		: base(tank) {
	}

	protected override void ControllerUpdate() {
		// Actions
		if (ActionMaster.GetAction(ActionCode.MoveForward)) {
			tank.MoveForward();
		}
		if (ActionMaster.GetAction(ActionCode.MoveBackward)) {
			tank.MoveBackward();
		}
		if (ActionMaster.GetAction(ActionCode.TurnLeft)) {
			tank.TurnLeft();
		}
		if (ActionMaster.GetAction(ActionCode.TurnRight)) {
			tank.TurnRight();
		}
		if (ActionMaster.GetAction(ActionCode.PrimaryFire)) {
			tank.Fire();
		}

		// Axes
		tank.LookHorizontal(ActionMaster.GetAxis(AxisCode.LookHorizontal));
		tank.LookVertical(ActionMaster.GetAxis(AxisCode.LookVertical));
	}
}
