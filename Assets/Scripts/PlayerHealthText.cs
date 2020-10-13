using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PlayerHealthText : MonoBehaviour
{
    private TMP_Text healthText;
    private int _health = 3;
    public int Health
    {
        set
        {
            _health = value;
            healthText.text = value.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TMP_Text>();
        if (!healthText)
        {
            Debug.LogWarning("Missing health text.");
        }
    }
}
