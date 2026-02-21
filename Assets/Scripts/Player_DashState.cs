using UnityEngine;

public class Player_DashState : EntityState
{
    float orignalGravityScale;
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        timerState = player.dashDuration;
        orignalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();
        timerState -= Time.deltaTime;

        player.SetVelocity(player.dashSpeed * player.facingDir, 0);


        if (timerState < 0)
        {
            if (player.isGrounded)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0,0);
        rb.gravityScale = orignalGravityScale;
    }
}
