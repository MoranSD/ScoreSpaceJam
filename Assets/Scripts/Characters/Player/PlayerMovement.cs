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
        public Vector3 moveDirection => _moveDirection;

        [Header("Input settings")]
        public bool mobileInput;
        [SerializeField] Joystick _uiJoystick;

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
            CheckHandleInput();
        }
        private void FixedUpdate()
        {
            Move();
            Rotate();
        }
        void CheckHandleInput()
        {
            if (mobileInput)
            {
                _moveDirection = _mainCamera.transform.forward * _uiJoystick.Vertical + _mainCamera.transform.right * _uiJoystick.Horizontal;
            }
            else
            {
                _moveDirection = _mainCamera.transform.forward * Input.GetAxisRaw("Vertical") + _mainCamera.transform.right * Input.GetAxisRaw("Horizontal");

                if (Input.GetKeyDown(KeyCode.Space)) Jump();
            }

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

        public bool IsGrounded()
        {
            Debug.DrawRay(transform.position + (Vector3.up * 0.2f), Vector3.down * 0.3f, Color.red);
            return Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, 0.3f, ~6);
        }
        public void Jump()
        {
            if (IsGrounded() == false) return;

            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
