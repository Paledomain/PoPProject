using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LongJumpState", menuName = "PlayerState/LongJump", order = 1)]
public class LongJumpState : PlayerState
{
    protected override void CustomStartState()
    {
    }

    protected override void CustomStateUpdate()
    {
        PlayerController.Instance.LongJump(mirrored ? -1.0f : 1.0f);
    }
}