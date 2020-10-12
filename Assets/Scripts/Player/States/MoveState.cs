using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveState", menuName = "PlayerState/Move", order = 1)]
public class MoveState : PlayerState
{
    [SerializeField]
    private float movementSpeed = 1.0f;

    [SerializeField]
    private Vector2 jumpForceFromState;

    public float MovementSpeed { get { return movementSpeed; } }

    public Vector2 CustomJumpForce { get { return jumpForceFromState; } }

    protected override void CustomStartState()
    {
    }

    protected override void CustomStateUpdate()
    {
        if (PlayerController.Instance.Grounded)
        {
            float multiplier = IsMirrored ? -1.0f : 1.0f;
            playerRigidBody.velocity = new Vector3(movementSpeed * multiplier, .0f, .0f);
        }
    }

    protected override bool IgnoreState()
    {
        bool leftMoving = buttons.Contains(GameButton.Left);
        if (leftMoving && !PlayerController.Instance.Mirrored)
        {
            return true;
        }
        if (!leftMoving && PlayerController.Instance.Mirrored)
        {
            return true;
        }
        return false;
    }
}