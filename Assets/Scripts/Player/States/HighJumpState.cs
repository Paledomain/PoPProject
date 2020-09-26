using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighJumpState", menuName = "PlayerState/HighJump", order = 1)]
public class HighJumpState : PlayerState
{
    protected override void CustomStartState()
    {
    }

    protected override void CustomStateUpdate()
    {
        PlayerController.Instance.HighJump(mirrored ? -1.0f : 1.0f);
    }
}