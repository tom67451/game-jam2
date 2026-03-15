using UnityEngine;

public class Twinspit : MonoBehaviour
{
    float damage = TwinShoot.damage;
    public Player player;

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
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            Destroy(gameObject);
            player.hp -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}