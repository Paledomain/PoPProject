using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathType
{
    Fall, Trap
}

[RequireComponent(typeof(Animator), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ClimbPointTracker climbPointUp;
    [SerializeField]
    private ClimbPointTracker climbPointDown;
    [SerializeField]
    private PlayerState defaultState;
    [SerializeField]
    private PlayerState fallingState;
    [SerializeField]
    private PlayerState trapDeathState;
    [SerializeField]
    private PlayerState fallDeathState;
    [SerializeField]
    private PlayerGroundChecker groundChecker;
    
    private Animator animator;
    private PlayerState previousState;
    private PlayerState currentState;
    private Vector3 previousPosition;

    // How many frames the transform should move to other direction before mirroring? This is to avoid flickering.
    int framesUntilMirror = 5;
    private bool _mirrored = false;

    // There should be only one player object in the scene, access it as a singleton.
    public static PlayerController Instance { get; private set; }

    public bool Grounded
    {
        get 
        {
            return groundChecker.GroundTouches > 0;
        }
    }

    public bool Dead { get { return PlayerHealth.Instance.Health <= 0; } }

    public bool Mirrored
    {
        get { return _mirrored; }
        private set 
        {
            if (value == _mirrored)
                return;

            _mirrored = value;
            float newXScale = value ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);
        }
    }

    public float PureVelocitySquared
    {
        get { return (transform.position - previousPosition).sqrMagnitude; }
    }
    public float PureVelocity
    {
        get { return (transform.position - previousPosition).magnitude; }
    }

    private void Start()
    {
        // Setup the singleton
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        animator = GetComponent<Animator>();

        if (!FindObjectOfType<InputMapper>())
        {
            Debug.LogWarning("Missing InputMapper, add one to the hierarchy.");
        }
        if (!defaultState)
        {
            Debug.LogWarning("Missing default state for the player.");
        }
        if (!groundChecker)
        {
            Debug.LogWarning("Missing ground checker from player.");
        }

        ChangeToDefaultState();
    }

    private void Update()
    {
        // Change mirrored status
        bool leftPressed = InputMapper.Instance.GetKey(GameButton.Left);
        bool rightPressed = InputMapper.Instance.GetKey(GameButton.Right);
        if (leftPressed && !rightPressed)
        {
            DelayedSetMirror(true);
        }
        else if (rightPressed && !leftPressed)
        {
            DelayedSetMirror(false);
        }
        else if (leftPressed && rightPressed)
        {
            DelayedSetMirror(false, true);
        }
        currentState.UpdateState();
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public void ChangeState(PlayerState boundState)
    {
        if (previousState)
        {
            Destroy(previousState);
        }
        if (currentState)
        {
            currentState.ExitState();
            previousState = currentState;
        }
        currentState = ScriptableObject.Instantiate(boundState);
        currentState.StartState(animator, previousState);
    }

    public void ChangeToDefaultState()
    {
        if (!Dead)
            ChangeState(defaultState);
    }

    public void ChangeToFallingState()
    {
        ChangeState(fallingState);
    }

    public void ForceToggleMirrored()
    {
        Mirrored = !Mirrored;
    }

    public bool ClimbUpAvailable(out Vector2 climbPointPos)
    {
        if (!climbPointUp)
        {
            climbPointPos = Vector2.zero;
            return false;
        }
        return climbPointUp.ClimbAvailable(Mirrored ? ClimbDirection.Left : ClimbDirection.Right, out climbPointPos);
    }

    public bool ClimbDownAvailable(out Vector2 climbPointPos)
    {
        if (!climbPointDown)
        {
            climbPointPos = Vector2.zero;
            return false;
        }
        return climbPointDown.ClimbAvailable(Mirrored ? ClimbDirection.Left : ClimbDirection.Right, out climbPointPos);
    }

    public void Die(DeathType type)
    {
        if (PlayerHealth.Instance.Health > 0)
            Debug.LogWarning("Health is supposed to be 0 before calling the Die() method.");

        switch (type)
        {
            case DeathType.Trap:
                ChangeState(trapDeathState);
                break;
            default:
                ChangeState(fallDeathState);
                break;
        }
    }

    private void DelayedSetMirror(bool mirror, bool ignoreFirstParam = false)
    {
        if ((mirror || ignoreFirstParam) && transform.position.x < previousPosition.x && !Mirrored)
        {
            framesUntilMirror--;
            if (framesUntilMirror == 0)
            {
                Mirrored = true;
                framesUntilMirror = 5;
            }
        }
        else if ((!mirror || ignoreFirstParam)&& transform.position.x > previousPosition.x && Mirrored)
        {
            framesUntilMirror--;
            if (framesUntilMirror == 0)
            {
                Mirrored = false;
                framesUntilMirror = 5;
            }
        }
    }
}
