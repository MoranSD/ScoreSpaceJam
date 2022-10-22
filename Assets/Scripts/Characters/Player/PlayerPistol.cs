using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Things;

namespace Player
{
    public class PlayerPistol : PlayerInventoryItemLocator
    {
        [SerializeField] GameObject _pistolObject;

        bool _isHavePistol;

        private void Start()
        {
            _pistolObject.SetActive(false);
        }

        private void Update()
        {
            if (_isHavePistol == false) return;

            Vector3 shootDirection = _pistolObject.transform.forward;
        }

        protected override void OnPlayerTakeItem(ItemType type)
        {
            if(type == ItemType.Pistol)
            {
                _pistolObject.SetActive(true);
                _isHavePistol = true;
            }
        }
        protected override void OnPlayerDropItem(ItemType type)
        {
            if (type == ItemType.Pistol)
            {
                _pistolObject.SetActive(false);
                _isHavePistol = false;
            }
        }
    }
}
