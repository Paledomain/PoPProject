using UnityEngine;

[CreateAssetMenu(fileName = "OrientationSelectionState", menuName = "PlayerState/OrientationSelection", order = 1)]
public class OrientationSelectorState : PlayerState
{
    [SerializeField]
    private PlayerState leftVariation;
    [SerializeField]
    private PlayerState rightVariation;

    protected override void CustomStartState()
    {
        bool facingLeft = PlayerController.Instance.Mirrored;
        PlayerController.Instance.ChangeState(facingLeft ? leftVariation : rightVariation);
    }

    protected override void CustomStateUpdate()
    {
    }
}
