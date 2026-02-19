using UnityEngine;

public class Player_RunState : Player_GroundedState
{
    public Player_RunState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);

        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.isWalking && player.moveInput.x != 0)
        {
            stateMachine.ChangeState(player.walkState);
        }
    }
}
