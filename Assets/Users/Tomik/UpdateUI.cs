using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateUI : MonoBehaviour
{   
    public Player player;
    public Slider xpSlider;
    public Slider healthSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI killCountText;
    
    void Update()
    {
        float xpProgress = (float)player.xp / player.xpToNextLevel * 100f;
        xpSlider.value = xpProgress;
        healthSlider.value = player.hp / player.maxHp;
        levelText.text = "Level: " + player.level.ToString();
        killCountText.text = "Kills: " + player.killCount.ToString();



    }

}
