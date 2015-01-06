using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankMainGun : MonoBehaviour
    {
        private GameObject barrelTip;
        private GameObject gunFireFX;
        private GameObject shellSlot;
        private GameObject loadedShell;

        private void Awake()
        {
            barrelTip = transform.FindChild("barrelTip").gameObject;
            shellSlot = transform.FindChild("shellSlot").gameObject;
            gunFireFX = transform.FindChild("gunFireFX").gameObject;
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
                gunFireFX.particleSystem.Play();
                loadedShell.rigidbody.isKinematic = false;
                loadedShell.transform.parent = null;
                loadedShell.rigidbody.AddRelativeForce(0f, 0f, 50f, ForceMode.Impulse);
                loadedShell.rigidbody.AddRelativeTorque(0f, 0f, 10f, ForceMode.Impulse);
                loadedShell = null;
                animation.Play();
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
