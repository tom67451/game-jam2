using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 15f;

    private Vector2 movement;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.Set(Inputmanager.Movement.x, Inputmanager.Movement.y);

        rb.linearVelocity = movement * MoveSpeed;
    }
}
