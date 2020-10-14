using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private Gate connectedGate;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!connectedGate)
        {
            Debug.LogWarning("Missing the animator to trigger.");
            return;
        }

        if (collision.CompareTag("Char"))
        {
            connectedGate.OnPressurePlatePressed();
        }
        animator.SetBool("pressed", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!connectedGate)
        {
            Debug.LogWarning("Missing the animator to trigger.");
            return;
        }

        if (collision.CompareTag("Char"))
        {
            connectedGate.OnPressurePlateUnPressed();
        }
        animator.SetBool("pressed", false);
    }
}
