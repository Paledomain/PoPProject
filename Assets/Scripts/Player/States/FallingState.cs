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

    float fallStartY;

    protected override void CustomStartState()
    {
        falling = true;
        if (!lowSpeedRecoveryState || !highSpeedRecoveryState)
        {
            Debug.LogWarning("You should define falling recovery states.");
        }
        fallStartY = PlayerHealth.Instance.transform.position.y;
    }

    protected override void CustomStateUpdate()
    {
        if (PlayerController.Instance.Grounded)
        {
            PlayerHealth.Instance.ApplyFallDamage(fallStartY - PlayerHealth.Instance.transform.position.y);
            bool highSpeedFall = PlayerController.Instance.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 
                highSpeedThreshold * highSpeedThreshold;
            PlayerController.Instance.ChangeState(highSpeedFall ? highSpeedRecoveryState : lowSpeedRecoveryState);
        }
    }
}
