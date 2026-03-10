using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;

public class Shoot : MonoBehaviour
{
    public GameObject spit;
    public float speed;
    public GameObject player;
    public Player Player;
    public static float damage = 10f;
    public AudioSource spitsound;
    public float clocker;
    public float clocker2;
    public float attackSpeed;
    public float attackSpeed2;

    public float attackProgress;
    public float attackProgress2;

    public bool semiAuto;

    private void Start()
    {
        spitsound = player.GetComponent<AudioSource>();
    }

    void Update()
    {
        attackSpeed = Player.attackSpeed;

        clocker += Time.deltaTime;
        clocker2 += Time.deltaTime;

        if (attackProgress <= 0.5f)
        {
            attackProgress = clocker * attackSpeed;
        }

        if (attackProgress2 <= 1f)
        {
            attackProgress2 = clocker2 * attackSpeed2;
        }

        if ((Input.GetMouseButtonDown(0) && Player.dead == false) && (attackProgress >= 0.5f))
        {
            clocker = 0;
            attackProgress = 0;
            spitsound.Play();
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;


            float angle = Mathf.Atan2(clickPos.y, clickPos.x) * Mathf.Rad2Deg;


            Vector3 direction = (clickPos - player.transform.position).normalized;

            GameObject proj = Instantiate(spit,player.transform.position,Quaternion.Euler(0, 0, angle));

            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }

        if (semiAuto == true && Player.dead == false && attackProgress2 >= 1f)
        {
            clocker2 = 0;
            attackProgress2 = 0;
            spitsound.Play();
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;


            float angle = Mathf.Atan2(clickPos.y, clickPos.x) * Mathf.Rad2Deg;


            Vector3 direction = (clickPos - player.transform.position).normalized;

            GameObject proj = Instantiate(spit, player.transform.position, Quaternion.Euler(0, 0, angle));

            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }
    }
}
