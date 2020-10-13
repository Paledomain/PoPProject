using UnityEngine;

[CreateAssetMenu(fileName = "JumpUpState", menuName = "PlayerState/JumpUp", order = 1)]
public class JumpUpState : InterruptablePlayerState
{
    [SerializeField]
    private float preparationDuration = 0.3f;
    
    [SerializeField]
    private Vector2 impulse;

    private bool impulseApplied = false;
    private ClimbState nextClimbUpState;

    protected override void CustomStartState()
    {
        foreach (var state in possibleNextStates)
        {
            ClimbState cs = state as ClimbState;
            if (cs)
            {
                nextClimbUpState = cs;
                break;
            }
        }
    }

    protected override void ChildCustomStateUpdate()
    {
        if (!impulseApplied && ElapsedTime > preparationDuration)
        {
            impulseApplied = true;
            playerRigidBody.AddForce(impulse, ForceMode2D.Impulse);
        }
        Vector2 climbUpPos;
        if (ClimbUpAvailable() && PlayerController.Instance.ClimbUpAvailable(out climbUpPos))
        {
            PlayerController.Instance.ChangeState(nextClimbUpState);
        }
    }

    private bool ClimbUpAvailable()
    {
        if (nextClimbUpState)
        {
            bool buttonsPressed = true;
            foreach (var btn in nextClimbUpState.Buttons)
            {
                if (!InputMapper.Instance.GetKey(btn))
                {
                    buttonsPressed = false;
                    break;
                }
            }
            if (buttonsPressed)
                return true;
        }
        return false;
    }
}
