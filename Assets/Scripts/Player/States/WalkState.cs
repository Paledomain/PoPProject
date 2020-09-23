using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkState", menuName = "PlayerState/Walk", order = 1)]
public class WalkState : PlayerState
{
    protected override void CustomStartState()
    {
    }

    protected override void CustomStateUpdate()
    {
        PlayerController.Instance.Walk(mirrored ? -1.0f : 1.0f);
    }
}