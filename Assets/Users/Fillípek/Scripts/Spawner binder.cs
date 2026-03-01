using UnityEngine;

public class Spawnerbinder : MonoBehaviour
{
    public GameObject player;
    public Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        gameObject.transform.position = player.transform.position;
    }
}
