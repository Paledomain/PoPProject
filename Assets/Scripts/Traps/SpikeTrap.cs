using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (PlayerController.Instance.Dead)
        {
            GetComponent<Animator>().SetBool("playerDead", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Char"))
        {
            GetComponent<Animator>().SetBool("pressed", true);
            if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Artemis_Run"))
            {
                PlayerHealth.Instance.ApplySpikeDamage(100);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Char"))
        {
            GetComponent<Animator>().SetBool("pressed", false);
        }
    }
}