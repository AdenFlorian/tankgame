using UnityEngine;

public enum TankControllerType {
	Player,
	AI
}

public class Tank : Actor {
	public GameObject tankTop;
	public GameObject tankGun { get; private set; }

	public TankController controller;

	public Renderer[] attachedRenderers;

	// Tank Components
	public new TankAudio audio;
	public new TankCamera camera;
	public TankDeath death;
	public TankDustFX dustFX;
	public TankHealth health;
	public TankMainGun mainGun;
	public TankMover mover;

	// Info for tankMover
	public TankMove tankMove = new TankMove();

	protected void Awake() {
	}

	private void Start() {
		tankGun = mainGun.gameObject;
	}

	public override void InitController(ControlledBy controllerType) {
		switch (controllerType) {
			case ControlledBy.PlayerLocal:
				controller = new TankControllerPlayer(this);
				camera = gameObject.AddComponent<TankCamera>();
				//GameMaster.playerTank = this;
				break;
			case ControlledBy.AI:
				controller = new TankControllerAI(this);
				foreach (Renderer rend in attachedRenderers) {
					rend.material = Resources.Load<Material>("tronAI");
				}
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
		//mover.MoveOrder(tankMove);
		//tankMove.Clear();
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

	public void LookHorizontal(float degrees) {
		tankMove.rotateGun = degrees;
	}

	public void LookVertical(float degrees) {
		tankMove.angleGun = degrees;
	}

	public void Fire() {
		mainGun.Fire();
	}
	public override void Damage(float amount) {
		health.Damage(amount);
	}
	public void OnZeroHP() {
		death.Explode();
		Destroy(gameObject);
	}

	void OnDestroy() {
		mainGun.OnActorDeath();
		if (controller.GetType() == typeof(TankControllerPlayer)) {
			MissionMaster.ReportMissionEvent(MissionEvent.PlayerDeath);
		} else {
			MissionMaster.ReportMissionEvent(MissionEvent.ActorDeath);
		}
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
	public float rotateGun;
	public float angleGun;

	public void Clear() {
		forth = false;
		back = false;
		left = false;
		right = false;
		rotateGun = 0;
		angleGun = 0;
	}
}
