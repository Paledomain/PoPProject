using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Climb", menuName = "PlayerState/Climb", order = 1)]
public class ClimbState : PlayerState
{
    private enum VerticalDirection { Up, Down }

    [SerializeField]
    private VerticalDirection direction;
    [SerializeField, Tooltip("Give the value for unmirrored (facing right) player orientation. It will be automatically mirrored.")]
    private Vector2 endPositionFromClimbPoint;

    private Vector2 climbStartPos;
    private Vector2 climbPoint;
    private Vector2 climbEndPos;
    bool startMirrored = false;

    protected override void CustomStartState()
    {
        climbStartPos = PlayerController.Instance.transform.position;
        // Populate the climb point member.
        if (direction == VerticalDirection.Up)
            PlayerController.Instance.ClimbUpAvailable(out climbPoint);
        else
            PlayerController.Instance.ClimbDownAvailable(out climbPoint);

        Vector2 climbEndPosWithDirection = PlayerController.Instance.Mirrored ? 
            new Vector2(-endPositionFromClimbPoint.x, endPositionFromClimbPoint.y) : 
            endPositionFromClimbPoint;
        
        climbEndPos = climbPoint + climbEndPosWithDirection;
        startMirrored = PlayerController.Instance.Mirrored;
    }

    protected override void CustomStateUpdate()
    {
        float progression = Mathf.SmoothStep(0, 1, ElapsedTime / duration);
        PlayerController.Instance.transform.position = climbStartPos + progression * (climbEndPos - climbStartPos);
    }

    // Ignore this state if the player is already facing the right direction.
    protected override bool IgnoreState()
    {
        Vector2 climbPointPos;
        if (direction == VerticalDirection.Down)
        {
            return !PlayerController.Instance.ClimbDownAvailable(out climbPointPos);
        }
        else
        {
            return !PlayerController.Instance.ClimbUpAvailable(out climbPointPos);
        }
    }
}