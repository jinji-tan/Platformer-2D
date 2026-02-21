using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("State Machine")]
    public StateMachine stateMachine;
    public Player_IdleState idleState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_WalkState walkState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }


    [Header("Input")]
    public PlayerInputSet input { get; private set; }
    public Vector2 moveInput { get; private set; }

    [Header("References")]
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    public SpriteRenderer sr { get; private set; }


    [Header("Player Info")]
    public bool isWalking { get; private set; }
    public bool isJumping { get; private set; }
    public bool isGrounded;
    public bool wallDetected;
    public int facingDir = 1;
    public bool facingRight = true;

    [Header("Movement Details")]
    public float moveSpeed = 10f;
    public float walkSpeed = 3f;
    public float jumpForce = 5f;
    public float inAirMultiplier = .7f;
    public float dashDuration = .25f;
    public float dashSpeed = 25f;
    public float wallSlideSpeed = 0.15f;

    [Header("Collision Detection")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float groudCheckDistance = 1f;
    [SerializeField] float wallCheckDistance = 1f;



    void Awake()
    {
        stateMachine = new StateMachine();
        input = new PlayerInputSet();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        runState = new Player_RunState(this, stateMachine, "run");
        walkState = new Player_WalkState(this, stateMachine, "walk");
        jumpState = new Player_JumpState(this, stateMachine, "jumpfall");
        fallState = new Player_FallState(this, stateMachine, "jumpfall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");

    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {

        stateMachine.UpdateActiveScene();
        HandleCollisionDectection();

    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Walk.performed += ctx => isWalking = true;
        input.Player.Walk.canceled += ctx => isWalking = false;

        input.Player.Jump.performed += ctx => isJumping = true;
        input.Player.Jump.canceled += ctx => isJumping = false;

    }

    private void OnDisable()
    {

        input.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled -= ctx => moveInput = Vector2.zero;

        input.Player.Walk.performed -= ctx => isWalking = true;
        input.Player.Walk.canceled -= ctx => isWalking = false;

        input.Player.Jump.performed += ctx => isJumping = true;
        input.Player.Jump.canceled += ctx => isJumping = false;


        input.Disable();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);

        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight)
        {
            Flip();
        }
        else if (xVelocity < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        facingDir *= -1;
    }

    void HandleCollisionDectection()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groudCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groudCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0f));
    }
}
