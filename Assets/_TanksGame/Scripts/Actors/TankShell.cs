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

        private void OnCollisionEnter(Collision collision)
        {
            // Make sure the shell didn't collide with the tank that fired it

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
