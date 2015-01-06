using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankMover : TankComponent
    {
        // Max values
        [Range(0, 1)]
        public float maxForward = 1f;
        [Range(0, 0.5f)]
        public float maxBackward = 2f;
        [Range(0, 4)]
        public float maxTurnRate = 1f;

        // Acceleration values
        public float accelForward = 0.5f;
        public float accelBackward = 0.5f;
        public float accelTurn = 1f;

        // Friction Values
        public float frictionTurn = 1.5f;
        public float frictionforward = 0.5f;

        // Current values
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

        private void Start()
        {
        }

        private void Update()
        {
            transform.position += transform.localToWorldMatrix.MultiplyVector(new Vector3(0f, 0f, currentForwardSpeed));

            transform.Rotate(0f, currentTurnRate, 0f, Space.Self);

            tankModel.speedNormalized = speedNormalized;
        }

        public void LookOrder(TankLook tankLook)
        {
            tankModel.tankTop.transform.Rotate(0f, tankLook.x, 0f, Space.Self);
            RotateGun(tankLook.y);
        }

        private void RotateGun(float y)
        {
            Transform gunTrans = tankModel.tankGun.transform;

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
    }
}
