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
    public GameObject enemy;

        public void AttackPlayer()
    {
        if (enemy.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerScript.hp -= damage;
            if (playerScript.hp <= 0)
            {
                // playerScript.Die();
            }
        }
    }


}
