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
        public GameObject tankTop;
        public GameObject tankGun { get; private set; }

        public TankController controller;

        // Tank Components
        public new TankAudio audio;
        public new TankCamera camera;
        public TankDustFX dustFX;
        public TankHitboxes hitboxes;
        public TankMainGun mainGun;
        public TankMover mover;

        public TankControllerType controlledBy;

        // Info for tankMover
        private TankMove tankMove = new TankMove();
        private TankLook tankLook = new TankLook();

        //public float speedNormalized = 0f;

        protected void Awake()
        {
            if (controlledBy == TankControllerType.Player) {
            } else {
            }
        }

        private void Start()
        {
            tankGun = mainGun.gameObject;
        }

        public override void InitController(ControllableBy controllerType)
        {
            switch (controllerType) {
                case ControllableBy.Player:
                    controller = gameObject.AddComponent<TankControllerPlayer>();
                    camera = gameObject.AddComponent<TankCamera>();
                    GameMaster.playerTank = this;
                    break;
                case ControllableBy.AI:
                    controller = gameObject.AddComponent<TankControllerAI>();
                    break;
                case ControllableBy.None:
                    break;
                default:
                    controller = gameObject.AddComponent<TankControllerAI>();
                    break;
            }
        }

        private void Update()
        {
            // TODO: Need to optimize
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
