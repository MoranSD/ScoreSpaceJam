using System.Collections.Generic;
using UnityEngine;
using Things;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public static event System.Action<ItemType> onTakeItem;
        public static event System.Action<ItemType> onDropItem;

        List<ItemHold> _items = new List<ItemHold>();

        private void Update()
        {
            if (_items.Count == 0) return;

            for (int i = _items.Count - 1; i >= 0; i--)
            {
                _items[i].currentActionTime -= Time.deltaTime;

                if (_items[i].currentActionTime <= 0)
                {
                    onDropItem?.Invoke(_items[i].type);
                    _items.RemoveAt(i);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item")) 
            {
                Item item = other.GetComponent<Item>();

                Debug.Log("Pick up item: " + item.type.ToString());

                onTakeItem?.Invoke(item.type);

                if (item.isNeedHold)
                {
                    ItemHold newItem = new ItemHold();
                    newItem.type = item.type;
                    newItem.currentActionTime = item.actionTime;

                    _items.Add(newItem);
                }

                Destroy(other.gameObject);
            }
        }
    }
    public class ItemHold
    {
        public ItemType type;
        public float currentActionTime;
    }
}
