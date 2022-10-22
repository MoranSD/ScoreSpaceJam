using Player.Inventory;
using Things;

namespace UI
{
    public class ScoreCounter : PlayerInventoryItemLocator
    {
        public static event System.Action onChanged;

        public static int score { get; private set; }

        private void Start() => score = 0;
        protected override void OnPlayerTakeItem(ItemType type)
        {
            if (type == ItemType.Coin)
            {
                score++;
                onChanged?.Invoke();
            }
        }
    }
}
