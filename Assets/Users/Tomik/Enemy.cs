using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Enemy : MonoBehaviour
{
    public Player player;
    public enum EnemyType
    {
        Basic,
        Boss
    }

    [Header("Enemy Settings")]
    public EnemyType enemyType;
    public float hp;
    public float maxHp;
    public float damage = 10f;

    public GameObject xp_orb;
    public GameObject playerObj;
    public EnemySpawningScript spawner;
    public HealthBar healthBar;
    void Awake() {
        maxHp = hp;
    }

    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawner = player.GetComponentInChildren<EnemySpawningScript>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy hit!");
            player.hp -= damage;
        }
        
    }

    private void Update()
    {   
        
        
        healthBar.SetHealth(hp, maxHp);
        
        
        if (hp <= 0f)
        {
            spawner.enemiesCurrentlyAlive--;
            player.killCount++;
            Instantiate(xp_orb, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
