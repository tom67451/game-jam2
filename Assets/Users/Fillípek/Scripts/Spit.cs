using UnityEngine;

public class Spit : MonoBehaviour
{
    public int damage;
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
            Destroy(gameObject);
            enemy.hp -= damage;
        }
    }
}
