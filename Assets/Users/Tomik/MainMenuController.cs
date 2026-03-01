using UnityEditor;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{   
    [SerializeField] private Switcher switcher;
    public void StartGame()
    {
        switcher.SwitchToScene("IntroCustcene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
