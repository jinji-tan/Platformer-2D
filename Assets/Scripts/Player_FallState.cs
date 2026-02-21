using Unity.VisualScripting;
using UnityEngine;

public class Player_FallState : Player_InAirState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.wallDetected && player.isGrounded == false)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
