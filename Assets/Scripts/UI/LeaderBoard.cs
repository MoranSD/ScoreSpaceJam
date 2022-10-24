using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

namespace GameUI
{
    public class LeaderBoard : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI[] _leaders;
        [SerializeField] TextMeshProUGUI _playerId;

        int _ourId;
        void StartSession(System.Action callBack)
        {
            LootLockerSDKManager.StartSession(GameManager.playerName, (response) => 
            {
                if (response.success)
                {
                    _ourId = response.player_id;
                    callBack();
                }
                else
                {
                    Debug.Log("Failed start session");
                }
            });

            
        }
        void SubmitScore(System.Action callBack)
        {
            LootLockerSDKManager.SubmitScore(GameManager.playerName, ScoreCounter.score, 8092, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Seccess submit score");
                    callBack();
                }
                else
                {
                    Debug.Log("Failed submit score");
                }
            });
        }
        void ShowScores()
        {
            LootLockerSDKManager.GetScoreList(8092, 5, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Seccess get score");

                    LootLockerLeaderboardMember[] scores = response.items;

                    for (int i = 0; i < scores.Length; i++)
                    {
                        string playerName;
                        if(scores[i].player.name != "")
                        {
                            playerName = scores[i].player.name;
                        }
                        else
                        {
                            playerName = scores[i].player.id.ToString();
                            if (scores[i].player.id == _ourId) _leaders[i].color = Color.green;
                        }
                        _leaders[i].text = playerName + ": " + scores[i].score;
                    }
                    if(scores.Length < 5)
                    {
                        for (int i = scores.Length; i < 5; i++)
                        {
                            _leaders[i].text = "none";
                        }
                    }
                }
                else
                {
                    Debug.Log("Failed get score");
                }
            });
        }
        public void ShowResults() => StartSession(() => { SubmitScore(() => { ShowScores(); }); });
    }
}
