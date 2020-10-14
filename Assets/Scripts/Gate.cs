using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private float openDuration = 5.0f;

    private Animator animator;
    private float plateUnpressedTime = -1.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (plateUnpressedTime > 0.0f && Time.time - plateUnpressedTime > openDuration)
        {
            animator.SetBool("openGate", false);
        }
    }

    public void OnPressurePlatePressed()
    {
        animator.SetBool("openGate", true);
        plateUnpressedTime = -1.0f;
    }

    public void OnPressurePlateUnPressed()
    {
        plateUnpressedTime = Time.time;
    }
}
