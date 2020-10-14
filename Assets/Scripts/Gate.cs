using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private float openDuration = 5.0f;
    [SerializeField]
    private BoxCollider2D colliderToMove;

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
            TryCloseGate();
        }
    }

    private void TryCloseGate()
    {
        Vector3 testPos = colliderToMove.transform.position;
        Vector2 testSize = colliderToMove.size * colliderToMove.transform.lossyScale;
        int castMask = 1 << 11;

        var playerUnder = Physics2D.OverlapBox(testPos, testSize, 0, castMask);
        if (!playerUnder)
        {
            animator.SetBool("openGate", false);
            colliderToMove.gameObject.SetActive(true);
            plateUnpressedTime = -1.0f;
        }
    }

    public void OnPressurePlatePressed()
    {
        colliderToMove.gameObject.SetActive(false);
        animator.SetBool("openGate", true);
        plateUnpressedTime = -1.0f;
    }

    public void OnPressurePlateUnPressed()
    {
        plateUnpressedTime = Time.time;
    }
}
