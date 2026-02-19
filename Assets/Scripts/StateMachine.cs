using UnityEngine;

public class StateMachine
{
    public EntityState currentState;

    public void Initialize(EntityState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        currentState.Exit();
        currentState=newState;
        currentState.Enter();
    }

    public void UpdateActiveScene()
    {
        currentState.Update();
    }
}

