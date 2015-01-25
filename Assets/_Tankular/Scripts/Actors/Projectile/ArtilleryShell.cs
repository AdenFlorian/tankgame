using System.Collections;
using UnityEngine;

public class ArtilleryShell : MonoBehaviour {

	public GameObject shellExplosionPrefab;

	private Vector3 launchPosition;
	private float distanceFromLaunch = 0f;
	private float armDistance = 2f;
	private TankShellState shellState = TankShellState.Pooled;
	private float damage = 10000f;
	private string OriginalName = "ArtilleryShell";

	private void Awake() {
	}

	private void Update() {
	}

	protected void FixedUpdate() {
		switch (shellState) {
			case TankShellState.Launched:
				distanceFromLaunch = (transform.position - launchPosition).magnitude;
				if (distanceFromLaunch > armDistance) {
					shellState = TankShellState.Armed;
				}
				break;
			case TankShellState.Armed:
				distanceFromLaunch = (transform.position - launchPosition).magnitude;
				break;
			default:
				break;
		}
	}

	public void Launch() {
		shellState = TankShellState.Launched;
		launchPosition = transform.position;
	}

	private void OnCollisionEnter(Collision collision) {
		Debug.Log(gameObject.name + " shell collided with: " + collision.transform.name);
		switch (shellState) {
			case TankShellState.Launched:
				Dud();
				break;
			case TankShellState.Armed:
				CheckCollision(collision);
				break;
			default:
				break;
		}
	}

	private void CheckCollision(Collision collision) {
		Actor actor = collision.gameObject.GetComponent<Actor>();
		if (actor != null) {
			actor.Damage(damage);
		} else if (true) {

		}
		Explode();
	}

	private void Explode() {
		shellState = TankShellState.Exploded;
		GameObject newExplosionGO = SpawnMaster.Instantiate(shellExplosionPrefab, transform.position, transform.rotation);
		Destroy(newExplosionGO, 5f);
		SendToPool();
	}

	private void Dud() {
		shellState = TankShellState.Dudded;
		rigidbody.velocity = Vector3.zero;
		StartCoroutine("DelayedSendToPool");
	}

	private IEnumerator DelayedSendToPool() {
		yield return new WaitForSeconds(5f);
		SendToPool();
	}

	public void SendToPool() {
		launchPosition = Vector3.zero;
		distanceFromLaunch = 0f;
		shellState = TankShellState.Pooled;
		gameObject.name = OriginalName;
		gameObject.SetActive(false);
		AmmoPool.Instance.ReturnShell(gameObject);
	}

	private enum TankShellState {
		Loaded,
		Launched,
		Armed,
		Exploded,
		Dudded,
		Pooled
	}
}
