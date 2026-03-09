
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;

public class Switcher : MonoBehaviour
{
    public static Switcher instance;
    public int currentFloorIndex = 0;

    public Animator fadeAnimator;

    bool clockClocker;
    float clocker;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeAnimator = GameObject.FindGameObjectWithTag("Fader").GetComponent<Animator>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero;
        }
    }

    private void Awake() {
        Debug.Log("Switcher Awake, currentFloorIndex = " + currentFloorIndex);

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        /*if (Input.GetKeyDown(KeyCode.L)) {
            SwitchToFloor(currentFloorIndex + 1);
        }
        */

        if (clockClocker)
        {
            clocker += Time.deltaTime;
        }

        if (clocker >= 0.3f)
        {
            fadeAnimator.SetBool("Start", false);
            fadeAnimator.SetBool("Continue", true);
            clockClocker = false;
            clocker = 0;
        }
    }

    public async Task SwitchToFloor(int newFloorIndex) {
        string sceneName = "Floor_" + newFloorIndex;

        Debug.Log("Switching to: " + sceneName);

        if (fadeAnimator != null)
        {
            fadeAnimator.SetBool("Start", true);
            fadeAnimator.SetBool("Continue", false);
            clockClocker = true;
        }

        await Task.Delay(300);

        if (Application.CanStreamedLevelBeLoaded(sceneName)) {
            currentFloorIndex = newFloorIndex;
            SceneManager.LoadScene(sceneName);
        } else {
            Debug.LogError("Add " + sceneName + " to Build Settings!");
        }
    }
    public void SwitchToScene(string sceneName) {
        if (fadeAnimator != null)
        {
            fadeAnimator.SetBool("Start", true);
            fadeAnimator.SetBool("Continue", false);
            clockClocker = true;
        }
        
        if (Application.CanStreamedLevelBeLoaded(sceneName)) {
            SceneManager.LoadScene(sceneName);
        } else {
            Debug.LogError("Add " + sceneName + " to Build Settings!");
        }
    }

}