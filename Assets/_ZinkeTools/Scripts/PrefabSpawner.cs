using UnityEngine;

namespace Zinke {
	public class PrefabSpawner : MonoBehaviour {
		public GameObject prefabA;
		public GameObject prefabB;
		public Transform resultParent;

		/*public int width;
		public int length;
		public int height;*/
		public int xCount = 10;
		public int yCount = 1;
		public int zCount = 5;

		public Vector3 origin;

		private void Awake() {
			if (resultParent == null) {
				resultParent = transform;
			}
			Generate();
		}

		private void Start() {
		}

		private void Update() {
		}

		private void Generate() {
			for (int x = 0; x < xCount; x++) {
				for (int y = 0; y < yCount; y++) {
					for (int z = 0; z < zCount; z++) {
						GameObject newGO = GameObject.Instantiate(prefabA, new Vector3(x, Mathf.Sin((x + z) * 0.1f) * 10, z), Quaternion.identity) as GameObject;
						newGO.transform.parent = resultParent;
					}
				}
			}
		}
	}
}
