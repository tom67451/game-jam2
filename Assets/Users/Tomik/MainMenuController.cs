using UnityEditor;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{   
    [SerializeField] private Switcher switcher;
    public void StartGame()
    {
        switcher.SwitchToScene("Floor_0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
