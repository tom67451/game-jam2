using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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
    [SerializeField] GameObject shootBar;
    [SerializeField] TwinShoot Shoot;

    public float clocker;

    private Vector2 movement;
    public Animator animator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerObj = GameObject.FindGameObjectWithTag("Player");

        maxHp = player.maxHp;
        //damage = Shoot.damage;
        regen = player.regenerationRate;
        attSpeed = player.attackSpeed;
        
        hp = maxHp;
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
        #region Animations
        movement.Set(Inputmanager.Movement.x, Inputmanager.Movement.y);

        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);
        #endregion

        clocker += Time.deltaTime;
        healthBar.SetHealth(hp, maxHp);

        #region Shootbar logic
        shootBar.transform.localScale = new Vector3(Shoot.attackProgress * 5, 0.05f, 0);
        #endregion

        #region Regen logic
        if (clocker >= 1 && hp < maxHp)
        {
            hp += regen;
            clocker = 0;
        }
        #endregion

        if (hp <= 0f)
        {
            player.killCount++;
            Instantiate(xp_orb, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Twin destroyed");
    }
}
