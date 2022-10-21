using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Things
{
    public enum ItemType
    {
        Coin,
        Pistol,
    }
    public class Item : MonoBehaviour
    {
        [field:SerializeField] public ItemType type { get; private set; }

        [SerializeField] float _turnSpeed;

        private void Update()
        {
            transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime);
        }
    }
}
