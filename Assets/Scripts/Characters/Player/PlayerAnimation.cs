using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] PlayerMovement _playerMovement;

        Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetFloat("isHaveGun", 0);
        }

        private void OnEnable()
        {
            PlayerInventory.onPickUpPistol += OnTakePistol;
            PlayerInventory.onDropPistol += OnDropPistol;
        }

        private void OnDisable()
        {
            PlayerInventory.onPickUpPistol -= OnTakePistol;
            PlayerInventory.onDropPistol -= OnDropPistol;
        }

        private void Update()
        {
            _animator.SetBool("isRun", _playerMovement.moveDirection != Vector3.zero);
        }

        void OnTakePistol() => _animator.SetFloat("isHaveGun", 1);
        void OnDropPistol() => _animator.SetFloat("isHaveGun", 0);
    }
}
