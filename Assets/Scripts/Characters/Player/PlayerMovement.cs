using UnityEngine;
using Player.Inventory;
using Things;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : PlayerInventoryItemLocator
    {
        public static event System.Action onJump;

        public Vector3 moveDirection => _moveDirection;

        [Header("move paramethers")]
        [SerializeField] float _baseMoveSpeed;
        [SerializeField] float _speedBoostMoveSpeed;
        [SerializeField] float _jumpForce;

        Vector3 _moveDirection;
        float _currentMoveSpeed;

        Camera _mainCamera;

        Rigidbody _rigidBody;

        private void Start()
        {
            _mainCamera = Camera.main;
            _rigidBody = GetComponent<Rigidbody>();
            _currentMoveSpeed = _baseMoveSpeed;
        }

        private void Update()
        {
            if (PlayerDeath.isDead) return;
            CheckHandleInput();
        }
        private void FixedUpdate()
        {
            if (PlayerDeath.isDead) return;
            Move();
            Rotate();
        }
        void CheckHandleInput()
        {
            _moveDirection = _mainCamera.transform.forward * Input.GetAxisRaw("Vertical") + _mainCamera.transform.right * Input.GetAxisRaw("Horizontal");
            _moveDirection.y = 0;
            _moveDirection.Normalize();

            if (Input.GetKeyDown(KeyCode.Space)) Jump();

        }
        void Move()
        {
            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _rigidBody.AddForce(_moveDirection * _currentMoveSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }
        void Rotate()
        {
            if (_moveDirection == Vector3.zero) return;

            transform.forward = _moveDirection;
        }
        private Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 currentPlayerVelocity = _rigidBody.velocity;
            currentPlayerVelocity.y = 0;

            return currentPlayerVelocity;
        }

        protected override void OnPlayerTakeItem(ItemType type)
        {
            if (type == ItemType.SpeedBoost)
            {
                _currentMoveSpeed = _speedBoostMoveSpeed;
            }
        }
        protected override void OnPlayerDropItem(ItemType type)
        {
            if (type == ItemType.SpeedBoost)
            {
                _currentMoveSpeed = _baseMoveSpeed;
            }
        }

        public bool IsGrounded() => UnityEngine.Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, 0.3f, ~6);
        public void Jump()
        {
            if (IsGrounded() == false) return;

            onJump?.Invoke();
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
