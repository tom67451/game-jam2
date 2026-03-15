using UnityEngine;

public class Spit : MonoBehaviour
{
    float damage = Shoot.damage;
    public Enemy enemy;
    public Twin twin;

    private float clocker;
    private void Update()
    {
        clocker += Time.deltaTime;

        if (clocker >= 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
            if (enemy != null )
            {
                Destroy(gameObject);
                enemy.hp -= damage;
            }
            else
            {
                twin = other.GetComponent<Twin>();
                Destroy(gameObject);
                twin.hp -= damage;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
