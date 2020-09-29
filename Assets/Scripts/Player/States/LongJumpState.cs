using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LongJumpState", menuName = "PlayerState/LongJump", order = 1)]
public class LongJumpState : PlayerState
{
    [SerializeField]
    private Vector2 jumpForce;

    protected override void CustomStartState()
    {
        playerRigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    protected override void CustomStateUpdate()
    {
    }
}