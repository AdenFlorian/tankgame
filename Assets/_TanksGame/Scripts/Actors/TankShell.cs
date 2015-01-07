using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankShell : MonoBehaviour
    {
        public GameObject shellExplosionPrefab;
        private Vector3 launchPosition;
        private bool launched = false;
        //private float distanceTraveled = 0f;
        private float distanceFromLaunch = 0f;
        private float armDistance = 5f;
        private bool armed = false;

        private void Start()
        {
        }

        private void Update()
        {
        }

        protected void FixedUpdate()
        {
            if (launched) {
                distanceFromLaunch = (transform.position - launchPosition).magnitude;
            }

            if (distanceFromLaunch > armDistance) {
                armed = true;
            }
        }

        public void Launch()
        {
            launched = true;
            launchPosition = transform.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Shell collided with: " + collision.transform.name);
            if (armed) {
                Explode();
            } else {
                Dud();
            }
        }

        private void Explode()
        {
            GameObject newExplosionGO = GameObject.Instantiate(shellExplosionPrefab, transform.position, transform.rotation) as GameObject;
            Destroy(newExplosionGO, 5f);
            SendToPool();
        }

        private void Dud()
        {
            rigidbody.velocity = Vector3.zero;
            StartCoroutine("DelayedSendToPool");
        }

        private IEnumerator DelayedSendToPool()
        {
            yield return new WaitForSeconds(5f);
            SendToPool();
        }

        private void SendToPool()
        {
            launchPosition = Vector3.zero;
            launched = false;
            //distanceTraveled = 0f;
            distanceFromLaunch = 0f;
            armed = false;
            transform.parent = AmmoPool.Instance.transform;
            rigidbody.isKinematic = true;
            gameObject.SetActive(false);
        }
    }
}
