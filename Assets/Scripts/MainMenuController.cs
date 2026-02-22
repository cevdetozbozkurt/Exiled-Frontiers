using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    public string gameSceneName = "Game";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenSettings()
    {
        Debug.Log("Settings Section!");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
