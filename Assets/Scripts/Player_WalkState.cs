using Unity.VisualScripting;
using UnityEngine;

public class Player_WalkState : Player_GroundedState
{
    public Player_WalkState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (player.isWalking)
        {
            player.SetVelocity(player.moveInput.x * player.walkSpeed, rb.linearVelocity.y);
        }
        else if (player.isWalking == false && player.moveInput.x != 0 )
        {
            stateMachine.ChangeState(player.runState);
        }

        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
