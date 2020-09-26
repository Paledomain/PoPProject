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
    private float jumpSpeed = 3.0f;

    [SerializeField]
    private float crouchSpeed = 1.0f;


    [SerializeField]
    private PlayerState defaultState;
    
    private Animator animator;
    private PlayerState currentState;

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

    public void Walk(float multiplier)
    {
        transform.position += new Vector3(walkSpeed * Time.deltaTime * multiplier, 0.0f, 0.0f);
    }

    public void Run(float multiplier)
    {
        transform.position += new Vector3(runSpeed * Time.deltaTime * multiplier, 0.0f, 0.0f);

    }

    public void HighJump(float multiplier)
    {
        transform.position += new Vector3(0.0f, jumpSpeed * Time.deltaTime * multiplier, 0.0f);

    }

    public void LongJump(float multiplier)
    {
        transform.position += new Vector3(jumpSpeed * Time.deltaTime * multiplier, jumpSpeed * Time.deltaTime / multiplier, 0.0f);

    }

    public void Crouch(float multiplier)
    {
        transform.position += new Vector3(crouchSpeed * Time.deltaTime * multiplier, 0.0f, 0.0f);
    }
}
