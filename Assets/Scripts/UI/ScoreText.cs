using UnityEngine;
using TMPro;

namespace GameUI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _score;
        private void Start() => OnScoreChanged();
        private void OnScoreChanged() => _score.text = "Score: " + ScoreCounter.score;
        private void OnEnable() => ScoreCounter.onChanged += OnScoreChanged;
        private void OnDisable() => ScoreCounter.onChanged -= OnScoreChanged;
    }
}
