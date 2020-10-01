using UnityEngine;

[CreateAssetMenu(fileName = "JumpUpState", menuName = "PlayerState/JumpUp", order = 1)]
public class JumpUpState : InterruptablePlayerState
{
    [SerializeField]
    private float preparationDuration = 0.3f;
    
    [SerializeField]
    private float firstAnimDuration = 0.6f;

    [SerializeField]
    private string secondAnimationName;
    
    [SerializeField]
    private Vector2 impulse;

    private bool impulseApplied = false;

    protected override void CustomStartState()
    {
    }

    protected override void ChildCustomStateUpdate()
    {
        if (!impulseApplied && ElapsedTime > preparationDuration)
        {
            impulseApplied = true;
            playerRigidBody.AddForce(impulse, ForceMode2D.Impulse);
        }
        
        if (ElapsedTime > firstAnimDuration)
        {
            animator.Play(secondAnimationName);
        }
    }
}
