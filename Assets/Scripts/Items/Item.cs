using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Things
{
    public enum ItemType
    {
        Coin,
        Pistol,
        SpeedBoost,
        Immortality,
    }
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemType type { get; private set; }
        [field: SerializeField] public float actionTime { get; private set; }
        [field: SerializeField] public bool isNeedHold { get; private set; }

        [SerializeField] float _turnSpeed;

        private void Update()
        {
            transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime);
        }
    }
}
