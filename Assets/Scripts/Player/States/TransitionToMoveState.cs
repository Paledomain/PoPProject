using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionToMoveState", menuName = "PlayerState/TransitionToMove", order = 1)]
public class TransitionToMoveState : PlayerState
{
    [SerializeField]
    private float targetSpeed = 1.0f;

    private float startVelocity = 0.0f;

    protected override void CustomStartState()
    {
        startVelocity = playerRigidBody.velocity.x;
    }

    protected override void CustomStateUpdate()
    {
        if (PlayerController.Instance.PureVelocitySquared > targetSpeed * targetSpeed)
            return;

        float movementSpeed = Mathf.SmoothStep(startVelocity, targetSpeed, ElapsedTime / duration);
        float multiplier = IsMirrored ? -1.0f : 1.0f;
        playerRigidBody.velocity = new Vector3(movementSpeed * multiplier, 0.0f, 0.0f);
    }
}