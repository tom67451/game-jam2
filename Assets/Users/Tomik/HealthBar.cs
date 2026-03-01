using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{   
    public Slider enemyHealthSlider;
    public void SetHealth(float hp, float maxHp)
    {
        enemyHealthSlider.value = hp / maxHp;
    }

    void Update()
    {
        
    }
}
