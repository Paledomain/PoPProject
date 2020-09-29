using UnityEngine;

[CreateAssetMenu(fileName = "FallingState", menuName = "PlayerState/Falling", order = 1)]
public class FallingState : PlayerState
{
    [SerializeField]
    PlayerState lowSpeedRecoveryState;
    [SerializeField]
    PlayerState highSpeedRecoveryState;
    [SerializeField]
    float highSpeedThreshold = 10.0f;
    [SerializeField]
    float deadlySpeedThreshold = 40.0f;

    protected override void CustomStartState()
    {
        falling = true;
        if (!lowSpeedRecoveryState || !highSpeedRecoveryState)
        {
            Debug.LogWarning("You should define falling recovery states.");
        }
    }

    protected override void CustomStateUpdate()
    {
        if (PlayerController.Instance.Grounded)
        {
            bool highSpeedFall = PlayerController.Instance.PureVelocitySquared > highSpeedThreshold * highSpeedThreshold;
            PlayerController.Instance.ChangeState(highSpeedFall ? highSpeedRecoveryState : lowSpeedRecoveryState);
        }
    }
}
