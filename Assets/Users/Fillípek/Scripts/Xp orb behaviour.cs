using UnityEngine;

public class Xporbbehaviour : MonoBehaviour
{
    public Player player;

    public int xp_value;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.xp += xp_value;
        }
        Debug.Log("Orb collected");
        Destroy(gameObject);
        
    }
}
