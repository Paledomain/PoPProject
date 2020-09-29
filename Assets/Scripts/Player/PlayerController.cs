using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerState defaultState;
    
    private Animator animator;
    private PlayerState currentState;

    // There should be only one player object in the scene, access it as a singleton.
    public static PlayerController Instance { get; private set; }

    public bool Falling { get; private set; }
    public bool Dead { get; private set; }

    private bool _mirrored = false;
    public bool Mirrored
    {
        get { return _mirrored; }
        set 
        { 
            _mirrored = value;
            float newXScale = value ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);
        }
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

        ChangeToDefaultState();
    }

    private void Update()
    { 
        currentState.UpdateState();
    }

    public void ChangeState(PlayerState boundState)
    {
        if (currentState)
        {
            currentState.ExitState();
            Destroy(currentState);
        }
        currentState = ScriptableObject.Instantiate(boundState);
        currentState.StartState(animator);
    }

    public void ChangeToDefaultState()
    {
        ChangeState(defaultState);
    }
}
