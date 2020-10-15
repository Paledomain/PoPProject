using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnevenFloor : MonoBehaviour
{
    private Animator animator;
    private bool activated = false;
    float deactivateTime = Mathf.Infinity;
    bool falling = false;
    Rigidbody2D rigidBody;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>(); ;
    }

    private void Update()
    {
        if (activated && Time.time > deactivateTime)
        {
            Destroy(gameObject);
        }

        if (falling && rigidBody)
        {
            if (rigidBody.velocity.sqrMagnitude < 0.01f)
            {
                animator.SetTrigger("break");
            }
        }
    }

    public void ActivateFloorTrap()
    {
        if (activated)
            return;
        
        Invoke("Drop", 1.0f);
        activated = true;
    }

    void Drop()
    {
        GetComponent<Collider2D>().enabled = false;
        // Scale so it doesn't get stuck on the edges.
        transform.localScale *= 0.95f;
        if (rigidBody)
        {
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            deactivateTime = Time.time + 2.0f;
        }
        falling = true;
        animator.SetTrigger("fall");
    }
}
