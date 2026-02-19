using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0f, rb.linearVelocity.y);
    }
    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0 && player.isWalking == false && player.isGrounded && player.isJumping == false)
        {
            stateMachine.ChangeState(player.runState);
        }
        else if (player.isWalking && player.moveInput.x != 0 && player.isGrounded && player.isJumping == false)
        {
            stateMachine.ChangeState(player.walkState);
        }

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
    }
}
