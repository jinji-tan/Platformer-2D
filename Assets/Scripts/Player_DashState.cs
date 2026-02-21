using UnityEngine;

public class Player_DashState : EntityState
{
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        timerState = player.dashDuration;
    }

    public override void Update()
    {
        base.Update();
        timerState -= Time.deltaTime;

        player.SetVelocity(player.dashSpeed * player.facingDir, rb.linearVelocity.y);


        if (timerState < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
