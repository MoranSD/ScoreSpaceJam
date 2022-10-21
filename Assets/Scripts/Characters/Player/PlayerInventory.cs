using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Things;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public static event System.Action onPickUpCoin;
        public static event System.Action onPickUpPistol;
        public static event System.Action onDropPistol;

        public int coinsCount { get; private set; }
        public bool isHavePistol { get; private set; }

        [SerializeField] float _pistolTimeInHands;
        float _currentPistolTime;

        private void Update()
        {
            if (isHavePistol == false) return;

            _currentPistolTime -= Time.deltaTime;

            if(_currentPistolTime <= 0)
            {
                isHavePistol = false;
                _currentPistolTime = _pistolTimeInHands;
                onDropPistol?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item")) 
            {
                Item item = other.GetComponent<Item>();

                Debug.Log("Pick up item: " + item.type.ToString());

                switch (item.type)
                {
                    case ItemType.Coin:
                        coinsCount++;
                        onPickUpCoin?.Invoke();
                        break;
                    case ItemType.Pistol:
                        _currentPistolTime = _pistolTimeInHands;
                        isHavePistol = true;
                        onPickUpPistol?.Invoke();
                        break;
                }

                Destroy(other.gameObject);
            }
        }
    }
}
