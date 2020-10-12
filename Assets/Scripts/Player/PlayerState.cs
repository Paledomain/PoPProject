using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class PlayerState : ScriptableObject
{
    private struct KeyBindToState
    {
        public KeyBindToState(PlayerState stateArg)
        {
            keys = stateArg.buttons;
            boundState = stateArg;
        }

        public List<GameButton> keys;
        public PlayerState boundState;

        public bool AllButtonsDown()
        {
            foreach (GameButton key in keys)
            {
                if (!InputMapper.Instance.GetKey(key))
                {
                    return false;
                }
            }
            return true;
        }
    }
    
    [SerializeField]
    protected List<PlayerState> possibleNextStates;
    [SerializeField]
    protected List<GameButton> buttons; 
    [SerializeField]
    private bool looping;
    [SerializeField, Tooltip("Duration in seconds, used only for un-looping states")]
    protected float duration = 1.0f;
    [SerializeField]
    private string animationName;

    private List<KeyBindToState> keyBinds;
    protected Animator animator;
    private float stateStartTime;
    protected Rigidbody2D playerRigidBody;
    protected bool falling = false;
    protected PlayerState _previousState;
    protected bool interruptable = false;

    protected float ElapsedTime
    {
        get { return Time.time - stateStartTime; }
    }

    protected bool IsMirrored
    {
        get
        {
            return buttons.Contains(GameButton.Left);
        }
    }

    protected PlayerState PreviousState
    {
        get
        {
            return _previousState ? _previousState : null;
        }
    }

    public List<GameButton> Buttons
    {
        get
        {
            return buttons;
        }
    }

    public void StartState(Animator animatorArg, PlayerState previousState)
    {
        animator = animatorArg;
        stateStartTime = Time.time;
        playerRigidBody = PlayerController.Instance.GetComponent<Rigidbody2D>();
        _previousState = previousState;
        interruptable = looping;

        {
            var keyBindList = new List<KeyBindToState>();
            foreach (PlayerState pState in possibleNextStates)
            {
                if (pState)
                    keyBindList.Add(new KeyBindToState(pState));
            }

            if (looping)
                keyBindList.Add(new KeyBindToState(this));

            // The keybinds with more buttons take higher priority, otherwise they would never be reached.
            keyBinds = keyBindList.OrderByDescending(keyBind => keyBind.keys.Count).ToList();
        }

        if (!string.IsNullOrEmpty(animationName))
            animator.Play(animationName);
        
        CustomStartState();
    }

    protected abstract void CustomStartState();

    public void UpdateState()
    {
        if (interruptable && !falling)
        {
            if (!PlayerController.Instance.Grounded)
            {
                PlayerController.Instance.ChangeToFallingState();
                return;
            }
            PlayerState desiredNextState = FindDesiredNextState();
            if (desiredNextState && desiredNextState != this)
            {
                PlayerController.Instance.ChangeState(desiredNextState);
                return;
            }
        }
        else if (!interruptable)
        {
            if (ElapsedTime > duration)
            {
                PlayerState desiredNextState = FindDesiredNextState();
                if (desiredNextState)
                {
                    PlayerController.Instance.ChangeState(desiredNextState);
                    return;
                }
                PlayerController.Instance.ChangeToDefaultState();
                return;
            }
        }

        CustomStateUpdate();
    }

    protected abstract void CustomStateUpdate();

    // States don't need to implement fixed update, but they are free to do so.
    public virtual void FixedUpdateState()
    {
    }

    protected virtual bool IgnoreState()
    {
        return false;
    }

    private PlayerState FindDesiredNextState()
    {
        if (keyBinds == null)
            return null;

        foreach (var keyBind in keyBinds)
        {
            if (keyBind.AllButtonsDown() && !keyBind.boundState.IgnoreState())
            {
                return keyBind.boundState;
            }
        }
        return null;
    }

    public void ExitState()
    {
    }
}
