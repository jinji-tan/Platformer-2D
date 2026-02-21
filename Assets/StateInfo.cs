using UnityEngine;
using TMPro;
using System;

public class StateInfo : EntityState
{
    // We removed [SerializeField] because this isn't a MonoBehaviour anymore
    private TextMeshProUGUI stateText;

    // Add stateText to the constructor arguments
    public StateInfo(Player player, StateMachine stateMachine, string animBoolName, TextMeshProUGUI _stateText)
        : base(player, stateMachine, animBoolName)
    {
        this.stateText = _stateText;
    }

    public override void Update()
    {
        base.Update();

        // Safety check to ensure the text reference exists
        if (stateText != null && stateMachine.currentState != null)
        {
            stateText.text = stateMachine.currentState.ToString();
        }
    }
}
