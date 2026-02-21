using UnityEngine;

public abstract class EntityState
{
    public Player player;
    public StateMachine stateMachine;
    protected string animBoolName { get; private set; }

    protected Animator anim { get; private set; }
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    public float timerState;


    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        rb = player.rb;
        anim = player.anim;
        input = player.input;
    }

    public virtual void Enter()
    {
        anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (input.Player.Dash.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.dashState);
        }

        // Debug.Log(stateMachine.currentState);
    }

    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }

    public bool CanDash()
    {
        return true;
    }
}
