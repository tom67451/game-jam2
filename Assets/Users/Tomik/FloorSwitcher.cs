
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FloorSwitcher : MonoBehaviour
{
    public static FloorSwitcher instance;
    public Animator transition;
    public float transitionTime = 1f;
    private int currentFloorIndex = 0;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        // Press L to test jump to next floor
        if (Input.GetKeyDown(KeyCode.L)) {
            SwitchToFloor(currentFloorIndex + 1);
        }
    }

    public void SwitchToFloor(int newFloorIndex) {
        string sceneName = "Floor_" + newFloorIndex;

        if (Application.CanStreamedLevelBeLoaded(sceneName)) {
            currentFloorIndex = newFloorIndex;
            StartCoroutine(PerformTransition(sceneName));
        } else {
            Debug.LogError("Add " + sceneName + " to Build Settings!");
        }
    }

    IEnumerator PerformTransition(string sceneName) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.Play("Fade_In", -1, 0f);
    }
}