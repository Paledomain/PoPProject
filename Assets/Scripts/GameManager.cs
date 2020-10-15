using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeLimitSeconds = 240.0f;
    [SerializeField]
    TMP_Text timeLeftText;

    private float endOfTime;

    private void Start()
    {
        endOfTime = Time.time + timeLimitSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputMapper.Instance.GetKey(GameButton.Restart))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        float timeRemaining = endOfTime - Time.time;
        if (timeRemaining > 0 && timeLeftText)
        {
            timeLeftText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
        else if (timeRemaining < 0)
        {
            // Show end screen (win or lose).
        }
    }
}
