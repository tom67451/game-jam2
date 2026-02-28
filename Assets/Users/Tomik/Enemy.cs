using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

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
    public float hp = 50f;
    public float damage = 10f;
    public float movementSpeed = 1.5f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;

    public GameObject xp_orb;
    public GameObject playerObj;
    public EnemySpawningScript spawner;

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
        if (hp <= 0f)
        {
            spawner.enemiesCurrentlyAlive--;
            Instantiate(xp_orb, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
