using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankShell : MonoBehaviour
    {
        public GameObject shellExplosionPrefab;
        private Vector3 launchPosition;
        //private float distanceTraveled = 0f;
        private float distanceFromLaunch = 0f;
        private float armDistance = 5f;
        private TankShellState shellState = TankShellState.Pooled;

        private void Start()
        {
        }

        private void Update()
        {
        }

        protected void FixedUpdate()
        {
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

        public void Launch()
        {
            shellState = TankShellState.Launched;
            launchPosition = transform.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Shell collided with: " + collision.transform.name);
            switch (shellState) {
                case TankShellState.Launched:
                    Dud();
                    break;
                case TankShellState.Armed:
                    Explode();
                    break;
                default:
                    break;
            }
        }

        private void Explode()
        {
            shellState = TankShellState.Exploded;
            GameObject newExplosionGO = GameObject.Instantiate(shellExplosionPrefab, transform.position, transform.rotation) as GameObject;
            Destroy(newExplosionGO, 5f);
            SendToPool();
        }

        private void Dud()
        {
            shellState = TankShellState.Dudded;
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
            //distanceTraveled = 0f;
            distanceFromLaunch = 0f;
            transform.parent = AmmoPool.Instance.transform;
            shellState = TankShellState.Pooled;
            rigidbody.isKinematic = true;
            gameObject.SetActive(false);
        }

        private enum TankShellState
        {
            Loaded,
            Launched,
            Armed,
            Exploded,
            Dudded,
            Pooled
        }
    }
}
