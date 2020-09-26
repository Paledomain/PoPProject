using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrouchState", menuName = "PlayerState/Crouch", order = 1)]
public class CrouchState : PlayerState
{
    protected override void CustomStartState()
    {
    }

    protected override void CustomStateUpdate()
    {
        PlayerController.Instance.Crouch(mirrored ? -1.0f : 1.0f);
    }
}