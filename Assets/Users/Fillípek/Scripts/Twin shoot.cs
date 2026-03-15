using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class TwinShoot : MonoBehaviour
{
    public GameObject spit;
    public float speed;
    public GameObject twin;
    public Twin Twin;
    public static float damage = 10f;
    public AudioSource spitsound;
    public float clocker;
    public float clocker2;
    public float attackSpeed;
    public float attackSpeed2;

    public float attackProgress;
    public float attackProgress2;

    public bool semiAuto;

    public GameObject player;

    private void Start()
    {
        spitsound = twin.GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        attackSpeed = Twin.attSpeed;

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

        if (attackProgress >= 0.5f)
        {
            clocker = 0;
            attackProgress = 0;
            spitsound.Play();
            Vector3 clickPos = player.transform.position;
            clickPos.z = 0;


            float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;


            Vector3 direction = (clickPos - twin.transform.position).normalized;

            GameObject proj = Instantiate(spit,twin.transform.position,Quaternion.Euler(0, 0, angle));

            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }

        if (semiAuto == true && attackProgress2 >= 1f)
        {
            clocker2 = 0;
            attackProgress2 = 0;
            spitsound.Play();
            Vector3 clickPos = player.transform.position;
            clickPos.z = 0;


            float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;


            Vector3 direction = (clickPos - twin.transform.position).normalized;

            GameObject proj = Instantiate(spit, twin.transform.position, Quaternion.Euler(0, 0, angle));

            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }
    }
}
