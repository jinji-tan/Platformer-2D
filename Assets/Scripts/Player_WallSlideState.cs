using UnityEngine;

public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        WallSlide();

        if (player.wallDetected == false)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public void WallSlide()
    {
        if (player.moveInput.y < 0)
        {
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        }
        else
        {
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideSpeed);
        }
    }
}
