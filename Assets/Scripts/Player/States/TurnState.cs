using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turn", menuName = "PlayerState/Turn", order = 1)]
public class TurnState : PlayerState
{
    protected override void CustomStartState()
    {
        PlayerController.Instance.ForceToggleMirrored();
    }

    protected override void CustomStateUpdate()
    {
    }

    // Ignore this state if the player is already facing the right direction.
    protected override bool IgnoreState()
    {
        if (!PlayerController.Instance.Mirrored)
        {
            return buttons.Contains(GameButton.Right);
        }
        else
        {
            return buttons.Contains(GameButton.Left);
        }
    }
}