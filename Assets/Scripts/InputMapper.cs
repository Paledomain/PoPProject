using System;
using UnityEngine;

public enum GameButton
{
    Left, Right, Up, Down, Shift, Restart
}
public class InputMapper : MonoBehaviour
{
    public static InputMapper Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            GameObject.Destroy(this.gameObject);
        } 
        else
        {
            Instance = this;
        }
    }

    public bool GetKey(GameButton btn)
    {
        switch (btn)
        {
            case GameButton.Left:
                return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            case GameButton.Right:
                return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            case GameButton.Up:
                return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            case GameButton.Down:
                return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
            case GameButton.Shift:
                return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            case GameButton.Restart:
                return Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Escape);
            default:
                return false;
        }
    }
}
