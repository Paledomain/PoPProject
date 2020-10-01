using UnityEngine;

public abstract class InterruptablePlayerState : PlayerState
{
    [SerializeField]
    private float interruptTime = 0.3f;

    [SerializeField]
    private PlayerState interruptState;

    protected override void CustomStateUpdate()
    {
        if (ElapsedTime < interruptTime && interruptState)
        {
            bool interruptKeysPressed = true;
            foreach (var btn in interruptState.Buttons)
            {
                if (!InputMapper.Instance.GetKey(btn))
                {
                    interruptKeysPressed = false;
                    break;
                }
            }
            if (interruptKeysPressed)
            {
                PlayerController.Instance.ChangeState(interruptState);
                return;
            }
        }

        ChildCustomStateUpdate();
    }

    protected abstract void ChildCustomStateUpdate();
}