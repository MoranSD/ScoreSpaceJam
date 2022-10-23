using UnityEngine;
using Player.Inventory;
using Things;

namespace Zombie
{
    [RequireComponent(typeof(Rigidbody))]
    public class ZombieMovement : PlayerInventoryItemLocator
    {
        public Vector3 moveDirection { get; private set; }

        [SerializeField] Collider _trigger;
        [SerializeField] float _moveSpeed;
        [SerializeField] float _idleTime;
        float _currentIdleTime;

        Rigidbody _rigidBody;
        Transform _playerTransform;

        bool _isPlayerHaveGun;

        protected override void OnEnable()
        {
            base.OnEnable();

            _currentIdleTime = _idleTime;
            _rigidBody = GetComponent<Rigidbody>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _playerTransform = player.transform;
            _isPlayerHaveGun = player.GetComponent<PlayerInventory>().IsHaveGun();
        }
        private void FixedUpdate()
        {
            _currentIdleTime -= Time.fixedDeltaTime;
            if (_currentIdleTime > 0) return;
            else _trigger.enabled = true;

            moveDirection = GetMoveDirection();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _rigidBody.AddForce(moveDirection * _moveSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);

            transform.forward = moveDirection;
        }
        Vector3 GetMoveDirection()
        {
            Vector3 moveDirection;

            if (_isPlayerHaveGun == false) moveDirection = _playerTransform.position - transform.position;
            else moveDirection = transform.position - _playerTransform.position;

            moveDirection.y = 0;
            moveDirection.Normalize();

            return moveDirection;
        }
        private Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 currentPlayerVelocity = _rigidBody.velocity;
            currentPlayerVelocity.y = 0;

            return currentPlayerVelocity;
        }

        protected override void OnPlayerTakeItem(ItemType type)
        {
            if (type == ItemType.Pistol) _isPlayerHaveGun = true;
        }
        protected override void OnPlayerDropItem(ItemType type)
        {
            if (type == ItemType.Pistol) _isPlayerHaveGun = false;
        }
    }

}