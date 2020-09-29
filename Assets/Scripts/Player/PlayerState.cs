using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class PlayerState : ScriptableObject
{
    struct KeyBindToState
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
    private List<PlayerState> possibleNextStates;
    [SerializeField]
    private List<GameButton> buttons; 
    [SerializeField]
    private bool looping;
    [SerializeField, Tooltip("Duration in seconds, used only for un-looping states")]
    protected float duration = 1.0f;
    [SerializeField]
    private string animationName;
    [SerializeField]
    protected bool mirrored;

    private List<KeyBindToState> keyBinds;
    protected Animator animator;
    private float stateStartTime;
    protected Rigidbody2D playerRigidBody;


    protected float ElapsedTime
    {
        get { return Time.time - stateStartTime; }
    }

    public void StartState(Animator animatorArg)
    {
        animator = animatorArg;
        stateStartTime = Time.time;
        playerRigidBody = PlayerController.Instance.GetComponent<Rigidbody2D>();

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

        PlayerController.Instance.Mirrored = mirrored;

        animator.Play(animationName);
        CustomStartState();
    }

    protected abstract void CustomStartState();

    public void UpdateState()
    {
        if (looping)
        {
            PlayerState desiredNextState = FindDesiredNextState();
            if (desiredNextState && desiredNextState != this)
            {
                PlayerController.Instance.ChangeState(desiredNextState);
                return;
            }
        }
        else
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

    private PlayerState FindDesiredNextState()
    {
        if (keyBinds == null)
            return null;

        foreach (var keyBind in keyBinds)
        {
            if (keyBind.AllButtonsDown())
            {
                return keyBind.boundState;
            }
        }
        return null;
    }

    public void ExitState()
    {
    }

    protected void PlayStateAnimation()
    {
        animator.Play(animationName);
    }
}
