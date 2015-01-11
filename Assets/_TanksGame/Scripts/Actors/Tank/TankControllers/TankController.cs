using UnityEngine;

public class TankController : ActorController {

	protected Tank tank;

	public TankController(Tank tank)
		: base(tank) {
		this.tank = tank;
	}
}
