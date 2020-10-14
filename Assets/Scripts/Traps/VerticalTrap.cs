using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTrap : MonoBehaviour
{
    [SerializeField]
    private float closeInterval = 2.5f;
    [SerializeField]
    private float closedTime = 0.6f;
    
    private Animator animator;
    private Collider2D trapCollider;
    private bool playerIn = false;

    // Start is called before the first frame update
    void Start()
    {
        trapCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        StartCoroutine("TrapInterval");
    }

    IEnumerator TrapInterval()
    {
        yield return new WaitForSeconds(closeInterval);
        while (!PlayerController.Instance.Dead)
        {
            CloseTrap();
            yield return new WaitForSeconds(closedTime);
            if (PlayerController.Instance.Dead)
                break;

            OpenTrap();
            yield return new WaitForSeconds(closeInterval - closedTime);
        }

    }

    void CloseTrap()
    {
        if (playerIn)
        {
            animator.SetBool("playerHit", true);
            PlayerHealth.Instance.ApplyTrapDamage(100);
            PlayerController.Instance.Die(DeathType.Trap);
        }
        else
        {
            animator.SetTrigger("closeTrap");
            trapCollider.isTrigger = false;
        }
    }

    void OpenTrap()
    {
        trapCollider.isTrigger = true;
        animator.SetTrigger("openTrap");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Char"))
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Char"))
        {
            playerIn = false;
        }
    }
}
