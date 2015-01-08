
public class TankHealth : TankComponent {
	public float healthPercent {
		get {
			if (hitPoints == 0) {
				return 0;
			} else {
				return maxHP / hitPoints;
			}
		}
	}

	private float _hitPoints;
	public float hitPoints {
		get {
			return _hitPoints;
		}
		private set {
			_hitPoints = value > 0 ? value : 0;
			if (value > 0) {
				_hitPoints = value;
			} else {
				_hitPoints = 0;
				tank.OnZeroHP();
			}
		}
	}
	public float maxHP { get; private set; }

	protected override void Awake() {
		base.Awake();
		maxHP = 1000;
		hitPoints = maxHP;
	}

	private void Start() {
	}

	private void Update() {
	}

	internal void Damage(float amount) {
		hitPoints -= amount;
	}
}
