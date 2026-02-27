using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

    public GameObject player;
    public Player playerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy hit!");
        playerScript.hp -= damage;
    }
}
