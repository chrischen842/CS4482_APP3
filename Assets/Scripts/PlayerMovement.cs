using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 9f;

    private Rigidbody2D _Rigidbody2D;
    private SpriteRenderer _SpriteRenderer;
    private Animator _Animator;
    private Vector2 movement;

    private enum MovementState { idle, running, jumping, falling, doubleJump };
    private MovementState state;
    private int jumpCount = 0;  // Counter for the number of jumps

    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        _Rigidbody2D.velocity = new Vector2(movement.x * moveSpeed, _Rigidbody2D.velocity.y);

        // Jump logic
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 2) // Allow double jump
            {
                _Rigidbody2D.velocity = new Vector2(_Rigidbody2D.velocity.x, jumpSpeed);
                jumpCount++;

                if (jumpCount == 2)
                {
                    state = MovementState.doubleJump;
                    _Animator.SetInteger("state", (int)state);
                }
            }
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (movement.x > 0f)
        {
            if(state != MovementState.doubleJump)
            {
                state = MovementState.running;
            }
            
            _SpriteRenderer.flipX = false;
        }
        else if (movement.x < 0f)
        {
            if (state != MovementState.doubleJump)
            {
                state = MovementState.running;
            }

            _SpriteRenderer.flipX = true;
        }
        else if (jumpCount == 0)
        {
            state = MovementState.idle;
        }

        if (_Rigidbody2D.velocity.y > 0.1f && jumpCount == 1)
        {
            state = MovementState.jumping;
        }
        else if (_Rigidbody2D.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        if (state != MovementState.doubleJump) // Avoid overriding doubleJump state
        {
            _Animator.SetInteger("state", (int)state);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump count when landing
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
    }
}
