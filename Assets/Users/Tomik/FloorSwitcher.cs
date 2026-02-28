using UnityEngine;

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

    
}
