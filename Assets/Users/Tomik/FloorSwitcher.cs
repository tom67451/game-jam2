/*using UnityEngine;

public class FloorSwitcher : MonoBehaviour
{
    public GameObject[] floors;
    private int currentFloorIndex = 0;

    void Start() {
        SwitchToFloor(0); // Ground floor
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            SwitchToFloor(1);
        }
    }

    public void SwitchToFloor(int newFloorIndex) {
        if (newFloorIndex < 0 || newFloorIndex >= floors.Length) return;

        for (int i = 0; i < floors.Length; i++) {
            floors[i].SetActive(i == newFloorIndex);
        }
        
        currentFloorIndex = newFloorIndex;
    }
}*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorSwitcher : MonoBehaviour
{
    private int currentFloorIndex = 0;

    [SerializeField] private string sceneNamePrefix = "Floor_"; 

    void Update() {
        // Just for testing
        if (Input.GetKeyDown(KeyCode.L)) {
            SwitchToFloor(currentFloorIndex + 1);
        }
    }

    public void SwitchToFloor(int newFloorIndex) {
        // Construct the string: "Floor_0", "Floor_1", etc.
        string sceneToLoad = sceneNamePrefix + newFloorIndex;

        if (Application.CanStreamedLevelBeLoaded(sceneToLoad)) {
            currentFloorIndex = newFloorIndex;
            SceneManager.LoadScene(sceneToLoad);
        } else {
            Debug.LogError("Scene " + sceneToLoad + " cannot be found! Check Build Settings.");
        }
    }

    public int GetCurrentFloorIndex() {
        return currentFloorIndex;
    }
}
