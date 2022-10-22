using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Things
{
    public class ItemCollector : MonoBehaviour
    {
        [SerializeField] Item[] _items;

        public Item GetRandomItem() => _items[Random.Range(0, _items.Length)];
    }
}
