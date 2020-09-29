using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerState defaultState;
    [SerializeField]
    private PlayerState fallingState;
    
    private Animator animator;
    private PlayerState previousState;
    private PlayerState currentState;
    private Vector3 previousPosition;

    private bool cachedGroundedValue;
    private bool groundedIsCached = false;
    private Vector3 groundedRaycastStartOffset;

    // How many frames the transform should move to other direction before mirroring? This is to avoid flickering.
    int framesUntilMirror = 5;
    private bool _mirrored = false;

    CapsuleCollider2D colliderComponent;
    // There should be only one player object in the scene, access it as a singleton.
    public static PlayerController Instance { get; private set; }

    public bool Grounded
    {
        get 
        {
            if (groundedIsCached)
                return cachedGroundedValue;

            int layerMask = 1 << 8;
            Vector3 raycastStart = transform.position + groundedRaycastStartOffset;
            cachedGroundedValue = Physics2D.Raycast(raycastStart, Vector3.down, 0.15f, layerMask);
            groundedIsCached = true;
            return cachedGroundedValue;
        }
    }

    public bool Dead { get; private set; }

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

        colliderComponent = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        {
            // We don't expect this to change so only calculate it once.
            Vector2 colliderBottomLocal = new Vector2(0.0f, -colliderComponent.size.y / 2 - colliderComponent.size.x / 2);
            Vector2 colliderBottom = (colliderComponent.offset + colliderBottomLocal);
            groundedRaycastStartOffset = new Vector3(colliderBottom.x * transform.localScale.x, colliderBottom.y * transform.localScale.y * 0.95f, 0.0f);
        }

        if (!FindObjectOfType<InputMapper>())
        {
            Debug.LogWarning("Missing InputMapper, add one to the hierarchy.");
        }
        if (!defaultState)
        {
            Debug.LogWarning("Missing default state for the player.");
        }

        ChangeToDefaultState();
    }

    private void Update()
    {
        groundedIsCached = false;
        // Change mirrored status with a short delay.
        if (transform.position.x < previousPosition.x && !Mirrored)
        {
            framesUntilMirror--;
            if (framesUntilMirror == 0)
            {
                Mirrored = true;
                framesUntilMirror = 5;
            }
        }
        else if (transform.position.x > previousPosition.x && Mirrored)
        {
            framesUntilMirror--;
            if (framesUntilMirror == 0)
            {
                Mirrored = false;
                framesUntilMirror = 5;
            }
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
}
