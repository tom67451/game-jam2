using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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

    private bool isPaused;
    private int rng;
    private int rng2;
    public int choiceTop = 3;
    public Shoot Shoot;

    public float clocker;
    private void Start()
    {
        choiceTop = upgrades.Length;
    }
    private void Update()
    {
        clocker += Time.deltaTime;

        if (clocker >= 1 && hp < maxHp)
        {
            hp += regenerationRate;
            clocker = 0;
        }

        if (hp <= 0)
        {   
            Destroy(gameObject);
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
}
