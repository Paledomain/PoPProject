using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum Action
    {
        Idle, WalkRight, WalkLeft, RunRight, RunLeft, JumpUp, JumpRight, JumpLeft, Down
    }

    [SerializeField]
    private float _runSpeed = 1.0f;

    private Animator animator;
    private Action desiredAction;

    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ReadInputs();
        PerformAction();
        UpdateAnimations();
        previousPosition = transform.position;
    }

    private void ReadInputs()
    {
        // Read inputs
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            desiredAction = Action.RunRight;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            desiredAction = Action.RunLeft;
        }
        // All other options
        // If there is no desired action, reset.
        else
        {
            desiredAction = Action.Idle;
        }
    }

    private void PerformAction()
    {
        switch (desiredAction)
        {
            case Action.RunRight:
                transform.position += new Vector3(_runSpeed * (Time.fixedDeltaTime / 1.0f), 0, 0);
                // Face right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
            case Action.RunLeft:
                transform.position += new Vector3(-_runSpeed * (Time.fixedDeltaTime / 1.0f), 0, 0);
                // Face left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
        }
    }

    private void UpdateAnimations()
    {
        float speed = (transform.position - previousPosition).magnitude / Time.deltaTime;
        animator.SetFloat("PedestrianSpeed", speed / _runSpeed);
    }
}
