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


    [Header("Input")]
    public PlayerInputSet input { get; private set; }
    public Vector2 moveInput { get; private set; }

    [Header("References")]
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    [Header("Movement Details")]
    public float moveSpeed = 10f;
    public float walkSpeed = 3f;
    public float jumpForce = 5f;
    public float inAirMultiplier = .7f;
    public bool isWalking { get; private set; }
    public bool isJumping { get; private set;}

    [Header("Collision Detection")]
    public bool isGrounded;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float groudCheckDistance = 1f;

    void Awake()
    {
        stateMachine = new StateMachine();
        input = new PlayerInputSet();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        runState = new Player_RunState(this, stateMachine, "run");
        walkState = new Player_WalkState(this, stateMachine, "walk");
        jumpState = new Player_JumpState(this, stateMachine, "jumpfall");
        fallState = new Player_FallState(this, stateMachine, "jumpfall");

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

        Flip();
    }

    public void Flip()
    {
        bool hasHorizontalVelocity = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f);
        }
    }

    void HandleCollisionDectection()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groudCheckDistance, whatIsGround);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groudCheckDistance));
    }
}
