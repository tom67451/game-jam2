using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public float regenerationRate = 5f;
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
    

    public Sprite Image1;
    public Sprite Image2;
    public Sprite Image3;

    private bool isPaused;
    private int rng;
    private int rng2;
    public int choiceTop = 3;
    private void Update()
    {
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

            level++;
            xpToNextLevel = xpToNextLevel * xpGrowthRate;
            totalxp += xp;
            xp = 0;

            #region Different options

            #region Option 1 ifs
            if (rng == 0)
            {
                Option1Name.text = "Blob of attacker";
                Option1Text.text = "Increases damage";
                Option1Image.sprite = Image1;
            }
            else if (rng == 1)
            {
                Option1Name.text = "Blob of health";
                Option1Text.text = "Increases health";
                Option1Image.sprite = Image2;
            }
            else if (rng == 2)
            {
                Option1Name.text = "Blob of speed";
                Option1Text.text = "Increases speed";
                Option1Image.sprite = Image3;
            }
            #endregion

            #region option 2 ifs
            if (rng2 == 0)
            {
                Option2Name.text = "Blob of attacker";
                Option2Text.text = "Increases damage";
                Option2Image.sprite = Image1;
            }
            else if (rng2 == 1)
            {
                Option2Name.text = "Blob of health";
                Option2Text.text = "Increases health";
                Option2Image.sprite = Image2;
            }
            else if (rng2 == 2)
            {
                Option2Name.text = "Blob of speed";
                Option2Text.text = "Increases speed";
                Option2Image.sprite = Image3;
            }
            #endregion
            #endregion
        }
    }

    public void Option1Click()
    {
        if (rng == 0)
        {
            Debug.Log("damage increased");
        }
        else if (rng == 1)
        {
            maxHp += 20;
        }
        else if (rng == 2)
        {
            movementSpeed += 1;
        }
        lvlUI.SetActive(false);
        ResumeGame();
    }

    public void Option2Click()
    {
        if (rng2 == 0)
        {
            Debug.Log("damage increased");
        }
        else if (rng2 == 1)
        {
            maxHp += 20;
        }
        else if (rng2 == 2)
        {
            movementSpeed += 20;
        }
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
