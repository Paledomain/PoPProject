using UnityEngine;

[CreateAssetMenu(fileName = "LongJumpState", menuName = "PlayerState/LongJump", order = 1)]
public class LongJumpState : PlayerState
{
    [SerializeField]
    private Vector2 defaultJumpForce;

    protected override void CustomStartState()
    {
        if (PlayerController.Instance.Grounded)
        {
            // Apply jump impulse depending on the previous state.
            playerRigidBody.velocity = new Vector2(0.0f, playerRigidBody.velocity.y);
            MoveState prevState = PreviousState as MoveState;
            // In some case, one might want to set the jump force to 0 but we don't expect that.
            if (prevState && prevState.CustomJumpForce.sqrMagnitude > 0.01f)
                playerRigidBody.AddForce(prevState.CustomJumpForce, ForceMode2D.Impulse);
            else
                playerRigidBody.AddForce(defaultJumpForce, ForceMode2D.Impulse);
        }
    }

    protected override void CustomStateUpdate()
    {
    }
}