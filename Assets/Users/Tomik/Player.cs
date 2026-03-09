using System.Net.Http.Headers;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Upgrades
{
    public string nadpis;
    public string popis;
    public Sprite image;
    public float damage;
    public float speed;
    public float health;
    public float regen;
}

public class Player : MonoBehaviour
{   
    
    [Header("Levels")]
    public int level = 1; 
    public float xp = 0f;
    public float xpToNextLevel = 100f;
    public float xpGrowthRate = 1.5f;
    public float totalxp = 0f;

    [Header("Movement")]
    public static float movementSpeed = 2f;

    [Header("Health")]
    public float hp = 100f;
    public float maxHp = 100f;
    public float regenerationRate = 0f;
    public bool invincible = false;

    [Header("Attack")]
    public float attackSpeed = 1f;
    [SerializeField] GameObject shootBar;

    [Header("Special abilites")]
    public float magnetRadius = 5f;
    public float magnetStrength = 10f;
    public float knockbackForce = 5f;

    [Header("UI Bindings")]
    public GameObject lvlUI;
    public Button Option1;
    public TextMeshProUGUI Option1Name;
    public TextMeshProUGUI Option1Text;
    public Image Option1Image;
    
    public Button Option2;
    public TextMeshProUGUI Option2Name;
    public TextMeshProUGUI Option2Text;
    public Image Option2Image;

    [SerializeField] public Upgrades[] upgrades;
    public int killCount = 0;

    private bool isPaused;
    private int rng;
    private int rng2;
    public int choiceTop = 3;
    public Shoot Shoot;
    public Switcher switcher;

    [Header("Death stuff")]
    [SerializeField] GameObject deathUi;
    private Animator deathAnimator;
    public bool dead = false;
    private GameObject spawner;
    private Light2D playerLight;
    private float lightClock;

    public float clocker;

    
    private void Start()
    {
        choiceTop = upgrades.Length;
        deathAnimator = deathUi.GetComponent<Animator>();
        switcher = GameObject.FindGameObjectWithTag("Switcher").GetComponent<Switcher>();
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        playerLight = gameObject.GetComponent<Light2D>();
    }
    private void Update()
    {
        clocker += Time.deltaTime;

        if (dead)
        {
            lightClock += Time.deltaTime;
        }

        shootBar.transform.localScale = new Vector3(Shoot.attackProgress * 5, 0.05f, 0);

        if (clocker >= 1 && hp < maxHp)
        {
            hp += regenerationRate;
            clocker = 0;
        }

        if (hp <= 0)
        {
            Die();
        }

        if (xp >= xpToNextLevel)
        {
            lvlUI.SetActive(true);
            PauseGame();

            rng = Random.Range(0, choiceTop);
            rng2 = Random.Range(0, choiceTop);
            while (rng2 == rng)
            {
                rng2 = Random.Range(0, choiceTop);
            }
            xp -= xpToNextLevel;
            level++;
            xpToNextLevel = xpToNextLevel * xpGrowthRate;
            totalxp += xp;
        }    

        if (dead && lightClock >= 0.1f && playerLight.intensity > 0)
        {
            lightClock = 0;
            playerLight.intensity -= 0.1f;
        }

        Option1Name.text = upgrades[rng].nadpis;
        Option1Text.text = upgrades[rng].popis;
        Option1Image.sprite = upgrades[rng].image;

        Option2Name.text = upgrades[rng2].nadpis;
        Option2Text.text = upgrades[rng2].popis;
        Option2Image.sprite = upgrades[rng2].image;
    }

    private static Player instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Option1Click()
    {
        Shoot.damage += upgrades[rng].damage;
        maxHp += upgrades[rng].health;
        movementSpeed += upgrades[rng].speed;
        regenerationRate += upgrades[rng].regen;
        lvlUI.SetActive(false);
        ResumeGame();
    }

    public void Option2Click()
    {
        Shoot.damage += upgrades[rng2].damage;
        maxHp += upgrades[rng2].health;
        movementSpeed += upgrades[rng2].speed;
        regenerationRate += upgrades[rng2].regen;
        lvlUI.SetActive(false);
        ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Die()
    {
        dead = true;
        movementSpeed = 0f;
        deathUi.SetActive(true);
        spawner.SetActive(false);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void Respawn()
    {
        dead = false;
        movementSpeed = 2f;
        deathUi.SetActive(false);
        hp = 100;
        maxHp = 100;
        level = 0;
        xp = 0;
        xpToNextLevel = 100f;
        regenerationRate = 0f;
        playerLight.intensity = 5f;
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        movementSpeed = 2f;
        hp = 100;
        maxHp = 100;
        level = 0;
        xp = 0;
        xpToNextLevel = 100f;
        regenerationRate = 0f;
        playerLight.intensity = 5f;
        deathUi.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
