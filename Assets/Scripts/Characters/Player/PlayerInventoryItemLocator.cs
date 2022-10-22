using UnityEngine;
using Things;

namespace Player
{
    public abstract class PlayerInventoryItemLocator : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            PlayerInventory.onTakeItem += OnPlayerTakeItem;
            PlayerInventory.onDropItem += OnPlayerDropItem;
        }
        protected virtual void OnDisable()
        {
            PlayerInventory.onTakeItem -= OnPlayerTakeItem;
            PlayerInventory.onDropItem -= OnPlayerDropItem;
        }
        protected abstract void OnPlayerTakeItem(ItemType type);
        protected virtual void OnPlayerDropItem(ItemType type) { }
    }
}
