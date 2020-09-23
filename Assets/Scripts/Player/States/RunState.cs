using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunState", menuName = "PlayerState/Run", order = 1)]
public class RunState : PlayerState
{
    protected override void CustomStartState()
    {
    }

    protected override void CustomStateUpdate()
    {
        PlayerController.Instance.Run(mirrored ? -1.0f : 1.0f);
    }
}