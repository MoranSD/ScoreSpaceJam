using Player.Inventory;
using LevelGeneration;
using Things;

namespace UI
{
    public class ScoreCounter : PlayerInventoryItemLocator
    {
        public static event System.Action onChanged;

        public static int score { get; private set; }

        private void Start() => score = 0;
        protected override void OnEnable()
        {
            base.OnEnable();

            LevelBlockStagesGeneration.onEndStage += OnEndStage;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            LevelBlockStagesGeneration.onEndStage -= OnEndStage;
        }
        void OnEndStage()
        {
            score += 10;
            onChanged?.Invoke();
        }
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
