
public class TankControllerPlayer : TankController {

	public TankControllerPlayer(Tank tank)
		: base(tank) {
	}

	protected override void ControllerUpdate() {
		// Actions
		if (Master.actionMaster.GetAction(ActionCode.MoveForward)) {
			tank.MoveForward();
		}
		if (Master.actionMaster.GetAction(ActionCode.MoveBackward)) {
			tank.MoveBackward();
		}
		if (Master.actionMaster.GetAction(ActionCode.TurnLeft)) {
			tank.TurnLeft();
		}
		if (Master.actionMaster.GetAction(ActionCode.TurnRight)) {
			tank.TurnRight();
		}
		if (Master.actionMaster.GetAction(ActionCode.PrimaryFire)) {
			tank.Fire();
		}

		// Axes
		tank.LookHorizontal(Master.actionMaster.GetAxis(AxisCode.LookHorizontal));
		tank.LookVertical(Master.actionMaster.GetAxis(AxisCode.LookVertical));
	}
}
