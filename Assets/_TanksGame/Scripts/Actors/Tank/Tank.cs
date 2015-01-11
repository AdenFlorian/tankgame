using UnityEngine;

public enum TankControllerType {
	Player,
	AI
}

public class Tank : Actor {
	public GameObject tankTop;
	public GameObject tankGun { get; private set; }

	public TankController controller;

	// Tank Components
	public new TankAudio audio;
	public new TankCamera camera;
	public TankDeath death;
	public TankDustFX dustFX;
	public TankHealth health;
	public TankMainGun mainGun;
	public TankMover mover;

	public TankControllerType controlledBy;

	// Info for tankMover
	private TankMove tankMove = new TankMove();
	private TankLook tankLook = new TankLook();

	protected void Awake() {
	}

	private void Start() {
		tankGun = mainGun.gameObject;
	}

	public override void InitController(ControlledBy controllerType) {
		switch (controllerType) {
			case ControlledBy.Player:
				controller = new TankControllerPlayer(this);
				camera = gameObject.AddComponent<TankCamera>();
				//GameMaster.playerTank = this;
				break;
			case ControlledBy.AI:
				controller = new TankControllerAI(this);
				break;
			case ControlledBy.Empty:
				controller = new TankController(this);
				break;
			default:
				controller = new TankControllerAI(this);
				break;
		}
		System.Diagnostics.Debug.Assert(controller != null);
	}

	private void Update() {
		// TODO: Need to optimize
		mover.MoveOrder(tankMove);
		tankMove.Clear();
		mover.LookOrder(tankLook);
		tankLook.Clear();
	}

	public void MoveForward() {
		tankMove.forth = true;
	}

	public void MoveBackward() {
		tankMove.back = true;
	}

	public void TurnLeft() {
		tankMove.left = true;
	}

	public void TurnRight() {
		tankMove.right = true;
	}

	public void LookHorizontal(float input) {
		tankLook.x = input;
	}

	public void LookVertical(float input) {
		tankLook.y = input;
	}

	public void Fire() {
		mainGun.Fire();
	}
	public override void Damage(float amount) {
		health.Damage(amount);
	}
	public void OnZeroHP() {
		mainGun.OnActorDeath();
		Master.missionMaster.ReportActorEvent(this, MissionEvent.ActorDeath);
		// death script to be called last, as it destroys the gameobject
		death.Die();
	}

	void OnDestroy() {
		//Debug.Log(this.actorID);
		controller.OnActorDeath();
		controller = null;
	}
}

public class TankMove {
	public TankMove() {
		Clear();
	}

	public bool forth;
	public bool back;
	public bool left;
	public bool right;

	public void Clear() {
		forth = false;
		back = false;
		left = false;
		right = false;
	}
}

public class TankLook {
	public TankLook() {
		Clear();
	}

	public float x;
	public float y;

	public void Clear() {
		x = 0f;
		y = 0f;
	}
}
