using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankCommander : MonoBehaviour
    {
        private TankMove tankMove = new TankMove();
        public TankDriver driver;

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
        }

        public void moveForward()
        {
            tankMove.forth = true;
        }

        public void moveBackward()
        {
            tankMove.back = true;
        }

        public void turnLeft()
        {
            tankMove.left = true;
        }

        public void turnRight()
        {
            tankMove.right = true;
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
}