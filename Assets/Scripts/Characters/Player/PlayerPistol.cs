using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerPistol : MonoBehaviour
    {
        [SerializeField] GameObject _pistolObject;

        private void Start()
        {
            _pistolObject.SetActive(false);
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
            
        }

        void OnTakePistol() => _pistolObject.SetActive(true);
        void OnDropPistol() => _pistolObject.SetActive(false);
    }
}
