
public abstract class TankComponent : ActorComponent {
	protected Tank tank;

	protected override void Awake() {
		base.Awake();

		tank = actor as Tank;

		var nameField = GetType().Name.Replace("Tank", "");
		nameField = nameField[0].ToString().ToLower()[0] + nameField.Substring(1);

		// Sets the field for this component in the tank object equal to this component
		typeof(Tank).GetField(nameField).SetValue(tank, this);
	}
}
