using UnityEngine;

public class TankController : ActorController {

	protected Tank tank;

	public Transform nextWaypoint;

	public TankMoveOrders state = TankMoveOrders.Hold;
	public RulesOfEngagement rulesOfEngage = RulesOfEngagement.CeaseFire;

	public TankController(Tank tank)
		: base(tank) {
		this.tank = tank;
	}
}

public enum TankMoveOrders {
	FollowTarget = 1,	// Staying within certain distance from target
	Hold = 2,
	SpinLeft = 3,
	SpinRight = 4
}

public enum RulesOfEngagement {
	OpenFire,
	CeaseFire
}
