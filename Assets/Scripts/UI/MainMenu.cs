using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

namespace GameUI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] TMP_InputField _playerName;
        public void StartGame()
        {
            if(_playerName.text.Length >= 6)
            {
                StringBuilder nextPlayerName = new StringBuilder(_playerName.text);
                nextPlayerName.Remove(6, nextPlayerName.Length - 6);
                GameManager.SetPlayerName(nextPlayerName.ToString());
            }
            else
            {
                if(_playerName.text.Length != 0) GameManager.SetPlayerName(_playerName.text);
            }

            GameManager.PlayGameScene();
        }
    }
}
