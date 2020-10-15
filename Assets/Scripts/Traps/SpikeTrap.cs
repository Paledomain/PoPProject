﻿using System.Collections;
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
            if (!PlayerController.Instance.SafeFromSpikes || !PlayerController.Instance.Grounded)
            {
                PlayerHealth.Instance.ApplyTrapDamage(100);
                // Stop immediately if spiked.
                PlayerController.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                PlayerController.Instance.GetComponent<Animator>().enabled = false;
                // PlayerController.Instance.GetComponent<Transform>().position += Vector3.down * Time.deltaTime;
                if (PlayerController.Instance.Grounded)
                {
                    PlayerController.Instance.GetComponent<Rigidbody2D>().isKinematic = true;
                }
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