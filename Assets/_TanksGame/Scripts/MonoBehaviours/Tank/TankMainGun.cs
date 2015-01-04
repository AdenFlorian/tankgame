using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankMainGun : MonoBehaviour
    {
        public GameObject barrelTip;
        public GameObject shellSlot;
        public GameObject loadedShell;

        private void Awake()
        {
        }

        private void Start()
        {
            LoadNewShell();
        }

        private void Update()
        {
        }

        public void Fire()
        {
            if (loadedShell != null) {
                barrelTip.audio.Play();
                loadedShell.rigidbody.isKinematic = false;
                loadedShell.transform.parent = null;
                loadedShell.rigidbody.AddRelativeForce(0f, 0f, 50f, ForceMode.Impulse);
                loadedShell = null;
                StartCoroutine("DelayedLoadShell");
            }
        }

        private IEnumerator DelayedLoadShell()
        {
            yield return new WaitForSeconds(1f);
            LoadNewShell();
        }

        private void LoadNewShell()
        {
            loadedShell = AmmoPool.Instance.GetNextShell();
            loadedShell.transform.parent = shellSlot.transform;
            loadedShell.transform.localPosition = Vector3.zero;
            loadedShell.transform.localRotation = Quaternion.identity;
            loadedShell.rigidbody.isKinematic = true;
            loadedShell.SetActive(true);
        }
    }
}
