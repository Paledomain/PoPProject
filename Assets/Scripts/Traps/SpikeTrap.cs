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
            if (!PlayerController.Instance.SafeFromSpikes && PlayerController.Instance.Grounded)
            {
                PlayerHealth.Instance.ApplySpikeDamage(100);
                // Stop immediately if spiked.
                PlayerController.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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