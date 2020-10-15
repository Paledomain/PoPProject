using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEnd : MonoBehaviour
{
    private TMP_Text endText;

    static public LevelEnd Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        endText = GetComponent<TMP_Text>();
    }

    public void ShowEndScreen(float time, bool victory, bool timeOut)
    {
        Time.timeScale = 0;
        string txt;
        if (victory)
        {
            txt = @"Congratulations!
                You escaped the dungeons";
        }
        else if (timeOut)
        {
            txt = @"You didn't make out in time.
                Grand Vizier Yaffar married your prince.";
        }
        else
        {
            txt = @"You died.";
        }

        txt += "\nTime: " + time.ToString("0.00") + " seconds";
        endText.text = txt;
    }
}
