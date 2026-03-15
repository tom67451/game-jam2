using Unity.VisualScripting;
using UnityEngine;

public class Twin : MonoBehaviour
{
    public Player player;

    [Header("Twin stats")]
    public float hp = 100f;
    public float maxHp;
    public float damage = 10f;
    public float regen;
    public float attSpeed;

    public GameObject xp_orb;
    public GameObject playerObj;
    public HealthBar healthBar;

    public float clocker;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerObj = GameObject.FindGameObjectWithTag("Player");

        hp = player.hp;
        maxHp = player.maxHp;
        //damage = Shoot.damage;
        regen = player.regenerationRate;
        attSpeed = player.attackSpeed;
        
        maxHp = hp;
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            player.hp -= damage;
        }
    }
    */

    private void Update()
    {
        clocker += Time.deltaTime;
        healthBar.SetHealth(hp, maxHp);

        #region Regen logic
        if (clocker >= 1 && hp < maxHp)
        {
            hp += regen;
            clocker = 0;
        }
        #endregion

        /*if (hp <= 0f)
        {
            player.killCount++;
            Instantiate(xp_orb, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
        */
    }

    void OnDestroy()
    {
        Debug.Log("Twin destroyed");
    }
}
