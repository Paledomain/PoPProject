using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "PlayerState/NewState", order = 1)]
public class PlayerState : ScriptableObject
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
    private float duration = 1.0f;
    [SerializeField]
    private List<string> animatorParameters;

    private List<KeyBindToState> keyBinds;
    protected Animator animator;
    private float stateStartTime;

    public void StartState(Animator animatorArg)
    {
        animator = animatorArg;
        stateStartTime = Time.time;
        if (looping)
        {
            var keyBindList = new List<KeyBindToState>();
            foreach (PlayerState pState in possibleNextStates)
                keyBindList.Add(new KeyBindToState(pState));

            if (looping)
                keyBindList.Add(new KeyBindToState(this));

            // The keybinds with more buttons take higher priority, otherwise they would never be reached.
            keyBinds = keyBindList.OrderByDescending(keyBind => keyBind.keys.Count).ToList();
        }

        SetAnimatorParameters();
    }

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
            float elapsedTime = Time.time - stateStartTime;
            if (elapsedTime > duration)
            {
                PlayerState desiredNextState = FindDesiredNextState();
                if (desiredNextState)
                {
                    PlayerController.Instance.ChangeState(desiredNextState);
                    return;
                }
                PlayerController.Instance.ChangeToDefaultState();
            }
        }
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
        ResetAnimatorParameters();
    }

    protected void SetAnimatorParameters()
    {
        foreach (string param in animatorParameters)
            animator.SetBool(param, true);
    }

    protected void ResetAnimatorParameters()
    {
        foreach (string param in animatorParameters)
            animator.SetBool(param, false);
    }
}
