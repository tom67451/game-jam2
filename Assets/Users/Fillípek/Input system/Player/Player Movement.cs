using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float MoveSpeed = Player.movementSpeed;

    private Vector2 movement;

    private Rigidbody2D rb;
    private Animator animator;


    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveSpeed = Player.movementSpeed;
        movement.Set(Inputmanager.Movement.x, Inputmanager.Movement.y);

        rb.linearVelocity = movement * MoveSpeed;

        animator.SetFloat(horizontal,movement.x);
        animator.SetFloat(vertical,movement.y);
    }
}
