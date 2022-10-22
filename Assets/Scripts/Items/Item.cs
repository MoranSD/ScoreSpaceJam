using UnityEngine;

namespace Things
{
    public enum ItemType
    {
        Coin,
        Pistol,
        SpeedBoost,
    }
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemType type { get; private set; }
        [field: SerializeField] public float actionTime { get; private set; }
        [field: SerializeField] public bool isNeedHold { get; private set; }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("KillBox")) Destroy(this.gameObject);
        }
    }
}
