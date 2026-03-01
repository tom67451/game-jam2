using UnityEngine;

public class Spawnerbinder : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        gameObject.transform.position = player.transform.position;
    }
}
