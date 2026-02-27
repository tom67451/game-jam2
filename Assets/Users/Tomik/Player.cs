using UnityEngine;

public class Player : MonoBehaviour
{   
    public Sprite playerSprite;
    [Header("Levels")]
    public int level = 1; 
    public float xp = 0f;
    public float xpToNextLevel = 100f;
    public float xpGrowthRate = 1.5f;

    [Header("Movement")]
    public float movementSpeed = 2f;

    [Header("Health")]
    public float hp = 100f;
    public float maxHp = 100f;
    public float regenerationRate = 5f;
    public bool invincible = false; 
    [Header("Special abilites")]
    public float magnetRadius = 5f;
    public float magnetStrength = 10f;
    public float knockbackForce = 5f;


}
