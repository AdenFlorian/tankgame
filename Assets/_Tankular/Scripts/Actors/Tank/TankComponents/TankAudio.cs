
public class TankAudio : TankComponent {
	protected void Update() {
		audio.pitch = tank.mover.speedNormalized;
	}
}
