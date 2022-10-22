using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Things;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : PlayerInventoryItemLocator
    {
        [SerializeField] PlayerMovement _playerMovement;

        Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetFloat("isHaveGun", 0);
            _animator.SetFloat("moveSpeed", 1);
        }

        private void Update()
        {
            _animator.SetBool("isRun", _playerMovement.moveDirection != Vector3.zero);
        }

        protected override void OnPlayerTakeItem(ItemType type)
        {
            switch (type)
            {
                case ItemType.Pistol:
                        _animator.SetFloat("isHaveGun", 1);
                        break;
                case ItemType.SpeedBoost:
                    _animator.SetFloat("moveSpeed", 2);
                    break;
            }
        }
        protected override void OnPlayerDropItem(ItemType type)
        {
            switch (type)
            {
                case ItemType.Pistol:
                    _animator.SetFloat("isHaveGun", 0);
                    break;
                case ItemType.SpeedBoost:
                    _animator.SetFloat("moveSpeed", 1);
                    break;
            }
        }
    }
}
