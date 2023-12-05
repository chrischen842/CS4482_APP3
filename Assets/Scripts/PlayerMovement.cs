using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 9f;
    public AudioSource jumpSound;
    public AudioSource runSound;

    private Rigidbody2D _Rigidbody2D;
    private SpriteRenderer _SpriteRenderer;
    private Animator _Animator;
    private Vector2 movement;

    private enum MovementState { idle, running, jumping, falling, doubleJump };
    private MovementState state;
    private int jumpCount = 0;
    private float runSoundTimer = 0f;
    private const float runSoundInterval = 0.5f;

    private void Awake()
    {
        jumpSound = GameObject.Find("JumpSound").GetComponent<AudioSource>();
        runSound = GameObject.Find("RunSound").GetComponent<AudioSource>();
    }

    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Animator = GetComponent<Animator>();

        _Animator.Play("PlayerSpawn");
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(movement.x) > 0.1f && Mathf.Abs(_Rigidbody2D.velocity.y) < 0.1f)
        {
            if (runSoundTimer <= 0f)
            {
                runSound.Play();
                runSoundTimer = runSoundInterval;
            }
        }
        else
        {
            runSoundTimer = 0f;
        }

        _Rigidbody2D.velocity = new Vector2(movement.x * moveSpeed, _Rigidbody2D.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 2)
            {
                _Rigidbody2D.velocity = new Vector2(_Rigidbody2D.velocity.x, jumpSpeed);

                jumpSound.Play();
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

        if (state != MovementState.doubleJump)
        {
            _Animator.SetInteger("state", (int)state);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FallingPlatform") || collision.gameObject.CompareTag("Platform"))
        {
            jumpCount = 0;
        }
    }
}
