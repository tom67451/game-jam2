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
}
