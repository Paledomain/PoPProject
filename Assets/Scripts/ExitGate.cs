using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Char"))
        {
            LevelEnd.Instance.ShowEndScreen(GameManager.Instance.GameTime, true, false);
        }
    }
}
