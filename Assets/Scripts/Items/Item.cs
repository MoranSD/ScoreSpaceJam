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
    }
}
