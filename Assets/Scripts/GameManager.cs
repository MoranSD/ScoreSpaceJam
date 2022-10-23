using UnityEngine.SceneManagement;

public static class GameManager
{
    public static event System.Action onRetryGame;
    public static string playerName = "Player";
    public static void SetPlayerName(string name) => playerName = name;
    public static void PlayGameScene()
    {
        onRetryGame?.Invoke();
        SceneManager.LoadScene(1);
    }
}
