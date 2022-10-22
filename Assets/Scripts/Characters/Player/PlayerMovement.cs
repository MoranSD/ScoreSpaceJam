using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Things;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : PlayerInventoryItemLocator
    {
        public bool mobileInput;
        public Vector3 moveDirection => _moveDirection;

        [SerializeField] Joystick _uiJoystick;
        [SerializeField] float _baseMoveSpeed;
        [SerializeField] float _speedBoostMoveSpeed;
        float _currentMoveSpeed;

        Vector3 _moveDirection;
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
            CheckHandleInput();
        }
        private void FixedUpdate()
        {
            Move();
            Rotate();
        }
        void CheckHandleInput()
        {
            if (mobileInput) _moveDirection = _mainCamera.transform.forward * _uiJoystick.Vertical + _mainCamera.transform.right * _uiJoystick.Horizontal;
            else _moveDirection = _mainCamera.transform.forward * Input.GetAxisRaw("Vertical") + _mainCamera.transform.right * Input.GetAxisRaw("Horizontal");

            _moveDirection.y = 0;
            _moveDirection.Normalize();
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
    }
}
