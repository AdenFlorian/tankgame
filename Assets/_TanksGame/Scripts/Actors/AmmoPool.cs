using UnityEngine;

public class AmmoPool : MonoBehaviour {
	public static AmmoPool Instance;

	public GameObject tankShellPrefab;

	public uint poolSize = 10;

	private GameObject[] tankShellPool;

	private uint shellIndex = 0;

	private void Awake() {
		tankShellPool = new GameObject[poolSize];
		Instance = this;

		for (int i = 0; i < poolSize; i++) {
			tankShellPool[i] = GameObject.Instantiate(tankShellPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tankShellPool[i].SetActive(false);
			tankShellPool[i].rigidbody.isKinematic = true;
			tankShellPool[i].transform.parent = transform;
		}
	}

	private void Start() {
	}

	private void Update() {
	}

	public GameObject GetNextShell() {
		if (shellIndex == poolSize - 1) {
			shellIndex = 0;
		} else {
			shellIndex++;
		}
		//tankShellPool[shellIndex].SetActive(true);
		return tankShellPool[shellIndex];
	}
}
