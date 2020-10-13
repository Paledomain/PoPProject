using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Char"))
        {
            GetComponent<Animator>().SetBool("pressed", true);
            if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Artemis_Run"))
            {
                Debug.Log("haha get spike trapped");
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