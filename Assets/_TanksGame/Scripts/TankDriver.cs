using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankDriver : MonoBehaviour
    {
        public GameObject tankTop;
        public GameObject tankGun;

        public float maxForward = 1f;
        public float maxBackward = 2f;
        public float maxTurnRate = 1f;

        public float accelForward = 0.5f;
        public float accelBackward = 0.5f;
        public float accelTurn = 1f;
        public float frictionTurn = 1.5f;
        public float frictionforward = 0.5f;

        public float currentForwardSpeed = 0f;
        public float currentTurnRate = 0f;

        public float speedNormalized
        {
            get
            {
                return currentForwardSpeed / maxForward;
            }
            private set
            {
                speedNormalized = value;
            }
        }

        // Main gun movement (look)
        public float maxLookUp;

        public float maxLookDown;
        private float gunAngle = 0f;

        private void Awake()
        {
        }

        private void Start()
        {
        }

        private void Update()
        {
            transform.position += transform.localToWorldMatrix.MultiplyVector(new Vector3(0f, 0f, currentForwardSpeed));

            transform.Rotate(0f, currentTurnRate, 0f, Space.Self);
        }

        public void MoveOrder(TankMove moveOrder)
        {
            if (moveOrder.forth) {
                currentForwardSpeed += accelForward * Time.deltaTime;
            } else if (moveOrder.back) {
                currentForwardSpeed -= accelBackward * Time.deltaTime;
            } else {
                if (currentForwardSpeed > 0) {
                    currentForwardSpeed -= frictionforward * Time.deltaTime;
                    currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, 0, maxForward);
                } else if (currentForwardSpeed < 0) {
                    currentForwardSpeed += frictionforward * Time.deltaTime;
                    currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, -maxBackward, 0);
                }
            }

            if (moveOrder.left) {
                currentTurnRate -= accelTurn * Time.deltaTime;
            } else if (moveOrder.right) {
                currentTurnRate += accelTurn * Time.deltaTime;
            } else {
                if (currentTurnRate > 0) {
                    currentTurnRate -= frictionTurn * Time.deltaTime;
                    currentTurnRate = Mathf.Clamp(currentTurnRate, 0, maxTurnRate);
                } else if (currentTurnRate < 0) {
                    currentTurnRate += frictionTurn * Time.deltaTime;
                    currentTurnRate = Mathf.Clamp(currentTurnRate, -maxTurnRate, 0);
                }
            }

            currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, -maxBackward, maxForward);
            currentTurnRate = Mathf.Clamp(currentTurnRate, -maxTurnRate, maxTurnRate);
        }

        public void LookOrder(TankLook tankLook)
        {
            tankTop.transform.Rotate(0f, tankLook.x, 0f, Space.Self);
            RotateGun(tankLook.y);
        }

        private void RotateGun(float y)
        {
            Transform gunTrans = tankGun.transform;

            gunTrans.Rotate(y, 0f, 0f, Space.Self);
            gunAngle += y;

            if (gunAngle > maxLookUp) {
                float adjustX = gunAngle - maxLookUp;
                gunTrans.Rotate(-adjustX, 0f, 0f, Space.Self);
                gunAngle += -adjustX;
            } else if (gunAngle < maxLookDown) {
                float adjustX = maxLookDown - gunAngle;
                gunTrans.Rotate(adjustX, 0f, 0f, Space.Self);
                gunAngle += adjustX;
            }
        }
    }
}