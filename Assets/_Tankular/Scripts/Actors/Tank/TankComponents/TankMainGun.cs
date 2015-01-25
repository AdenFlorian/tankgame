using System.Collections;
using UnityEngine;

public class TankMainGun : TankComponent {
	private GameObject barrelTip;
	private GameObject gunFireFX;
	private GameObject shellSlot;
	private GameObject loadedShell;

	private void Start() {
		barrelTip = transform.FindChild("barrelTip").gameObject;
		shellSlot = transform.FindChild("shellSlot").gameObject;
		gunFireFX = transform.FindChild("gunFireFX").gameObject;
		LoadNewShell();
	}

	private void Update() {
	}

	public void Fire() {
		if (loadedShell != null) {
			loadedShell.SetActive(true);
			loadedShell.transform.position = shellSlot.transform.position;
			loadedShell.transform.rotation = shellSlot.transform.rotation;
			loadedShell.rigidbody.isKinematic = false;
			loadedShell.rigidbody.velocity = Vector3.zero;
			loadedShell.rigidbody.AddRelativeForce(0f, 0f, 50f, ForceMode.VelocityChange);
			loadedShell.rigidbody.AddRelativeTorque(0f, 0f, 10f, ForceMode.VelocityChange);
			ArtilleryShell artShell = loadedShell.GetComponent<ArtilleryShell>();
			artShell.Launch();
			loadedShell.name += tank.actorID;
			loadedShell = null;
			StartCoroutine("DelayedLoadShell");

			barrelTip.audio.Play();
			gunFireFX.particleSystem.Play();
			animation.Play();
		} else {
			// Out of ammo
		}
	}

	private IEnumerator DelayedLoadShell() {
		yield return new WaitForSeconds(1f);
		LoadNewShell();
	}

	private void LoadNewShell() {
		loadedShell = AmmoPool.Instance.GetNextShell();
	}

	public override void OnActorDeath() {
		if (loadedShell != null) {
			loadedShell.GetComponent<ArtilleryShell>().SendToPool();
		}
	}
}
