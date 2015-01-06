using System.Collections;
using UnityEngine;

namespace Tank
{
    public enum TankControllerType
    {
        Player,
        AI
    }

    public class Tank : Actor
    {
        public GameObject tankTop { get; private set; }
        public GameObject tankGun { get; private set; }

        // Tank Components
        public TankAudio audio;
        public TankControllerAI controllerAI;
        public TankControllerPlayer controllerPlayer;
        public TankControllerPlayer dustFX;
        public TankControllerPlayer hitboxes;
        public TankMainGun mainGun;
        public TankMover mover;

        public TankControllerType controlledBy = TankControllerType.AI;

        // Info for tankMover
        private TankMove tankMove = new TankMove();
        private TankLook tankLook = new TankLook();

        //public float speedNormalized = 0f;

        protected override void Awake()
        {
            base.Awake();

            //mainGun = GetComponentInChildren<TankMainGun>();
            //mover = GetComponent<TankMover>();
            /*if (mover == null) {
                Debug.LogError("WTF");
            }*/
        }

        private void Start()
        {
            if (controlledBy == TankControllerType.Player) {
                GameMaster.playerTank = this;
            }
            tankGun = mainGun.gameObject;
        }

        private void Update()
        {
            mover.MoveOrder(tankMove);
            tankMove.Clear();
            mover.LookOrder(tankLook);
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
            mainGun.Fire();
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
