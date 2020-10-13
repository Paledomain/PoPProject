using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnevenFloor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Char"))
        {
            Invoke("Drop", 1.0f);
        }
    }

    void Drop()
    {
        Destroy(this.gameObject);
    }
}
