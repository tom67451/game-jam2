using UnityEditor;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{   
    public GameObject pauseMenuObject;
    public bool isPaused = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuObject.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuObject.SetActive(false);
    }
    public void QuitToDesktop()
    {
        Time.timeScale = 1f;
        Application.Quit();
        
    }
    public void QuitToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
