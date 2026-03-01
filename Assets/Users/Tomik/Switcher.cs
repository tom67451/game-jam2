
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Switcher : MonoBehaviour
{
    public static Switcher instance;
    private int currentFloorIndex = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            SwitchToFloor(currentFloorIndex + 1);
        }
    }

    public void SwitchToFloor(int newFloorIndex) {
        string sceneName = "Floor_" + newFloorIndex;

        if (Application.CanStreamedLevelBeLoaded(sceneName)) {
            currentFloorIndex = newFloorIndex;
            SceneManager.LoadScene(sceneName);
        } else {
            Debug.LogError("Add " + sceneName + " to Build Settings!");
        }
    }
    public void SwitchToScene(string sceneName) {
        if (Application.CanStreamedLevelBeLoaded(sceneName)) {
            SceneManager.LoadScene(sceneName);
        } else {
            Debug.LogError("Add " + sceneName + " to Build Settings!");
        }
    }

}