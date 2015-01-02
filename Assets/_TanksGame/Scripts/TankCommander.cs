using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankCommander : MonoBehaviour
    {
        public TankDriver driver;

        private TankMove tankMove = new TankMove();
        private TankLook tankLook = new TankLook();

        private void Awake()
        {
        }

        private void Start()
        {
        }

        private void Update()
        {
            driver.MoveOrder(tankMove);
            tankMove.Clear();
            driver.LookOrder(tankLook);
            tankLook.Clear();
        }

        public void MoveForward()
        {
            tankMove.forth = true;
        }

        public void MoveBackward()
        {
            tankMove.back = true;
        }

        public void TurnLeft()
        {
            tankMove.left = true;
        }

        public void TurnRight()
        {
            tankMove.right = true;
        }

        public void LookHorizontal(float input)
        {
            tankLook.x = input;
        }

        public void LookVertical(float input)
        {
            tankLook.y = input;
        }

        public void Fire()
        {
        }
    }

    public class TankMove
    {
        public TankMove()
        {
            Clear();
        }

        public bool forth;
        public bool back;
        public bool left;
        public bool right;

        public void Clear()
        {
            forth = false;
            back = false;
            left = false;
            right = false;
        }
    }

    public class TankLook
    {
        public TankLook()
        {
            Clear();
        }

        public float x;
        public float y;

        public void Clear()
        {
            x = 0f;
            y = 0f;
        }
    }
}