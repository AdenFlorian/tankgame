using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankShell : MonoBehaviour
    {
        public GameObject shellExplosionPrefab;

        private void Awake()
        {
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        private void OnCollisionEnter()
        {
            Explode();
        }

        private void Explode()
        {
            GameObject newExplosionGO = GameObject.Instantiate(shellExplosionPrefab, transform.position, transform.rotation) as GameObject;
            Destroy(newExplosionGO, 5f);
            gameObject.SetActive(false);
            transform.parent = AmmoPool.Instance.transform;
        }
    }
}
