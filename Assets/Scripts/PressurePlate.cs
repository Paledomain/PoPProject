using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private Gate connectedGate;

    [SerializeField]
    private bool isAntiPressurePlate = false;

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
            if (!isAntiPressurePlate)
            {
                connectedGate.OnPressurePlatePressed();
            }
            else
                connectedGate.AntiPlatePressed();

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
