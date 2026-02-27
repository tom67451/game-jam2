using UnityEditor;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{   

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
