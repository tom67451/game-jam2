using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Shoot : MonoBehaviour
{
    public GameObject spit;
    public float speed;
    public GameObject player;
    public static float damage = 10f;
    public AudioSource spitsound;

    private void Start()
    {
        spitsound = player.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spitsound.Play();
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;


            float angle = Mathf.Atan2(clickPos.y, clickPos.x) * Mathf.Rad2Deg;


            Vector3 direction = (clickPos - player.transform.position).normalized;

            GameObject proj = Instantiate(spit,player.transform.position,Quaternion.Euler(0, 0, angle));

            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }
    }
}
