using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

namespace GameUI
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] GameObject _menu;
        [SerializeField] LeaderBoard _leaderBoard;
        private void OnEnable() => PlayerDeath.onDead += OnPlayerDead;
        private void OnDisable() => PlayerDeath.onDead -= OnPlayerDead;
        private void Start() => _menu.SetActive(false);
        void OnPlayerDead()
        {
            _menu.SetActive(true);
            _leaderBoard.ShowResults();
        }
        public void TryAgain() => GameManager.PlayGameScene();
    }
}
