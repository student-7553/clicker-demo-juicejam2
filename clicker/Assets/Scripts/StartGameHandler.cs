using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameHandler : MonoBehaviour
{
    public string gameSceneName;

    public void handleStartGame()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}
