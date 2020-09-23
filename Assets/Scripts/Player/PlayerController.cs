using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1.0f;

    [SerializeField]
    private float runSpeed = 3.0f;

    [SerializeField]
    private PlayerState defaultState;
    
    private Animator animator;
    private PlayerState currentState;

    public static PlayerController Instance { get; private set; }

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

    internal void ChangeToDefaultState()
    {
        ChangeState(defaultState);
    }
}
