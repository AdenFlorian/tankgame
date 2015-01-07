using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankMainGun : TankComponent
    {
        private GameObject barrelTip;
        private GameObject gunFireFX;
        private GameObject shellSlot;
        private GameObject loadedShell;

        private void Start()
        {
            barrelTip = transform.FindChild("barrelTip").gameObject;
            shellSlot = transform.FindChild("shellSlot").gameObject;
            gunFireFX = transform.FindChild("gunFireFX").gameObject;
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
                loadedShell.SetActive(true);
                loadedShell.rigidbody.isKinematic = false;
                loadedShell.transform.parent = null;
                loadedShell.rigidbody.AddRelativeForce(0f, 0f, 50f, ForceMode.Impulse);
                //loadedShell.rigidbody.AddRelativeTorque(0f, 0f, 10f, ForceMode.Impulse);
                loadedShell.GetComponent<TankShell>().Launch();
                loadedShell = null;
                animation.Play();
                StartCoroutine("DelayedLoadShell");
            } else {
                // Out of ammo
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
        }
    }
}
