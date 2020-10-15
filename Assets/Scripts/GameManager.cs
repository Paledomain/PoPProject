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
    private float startTime;
    static public GameManager Instance { get; private set; }

    private void Start()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        endOfTime = Time.time + timeLimitSeconds;
        startTime = Time.time;
    }

    public float GameTime { get { return Time.time - startTime; } }

    // Update is called once per frame
    void Update()
    {
        if (InputMapper.Instance.GetKey(GameButton.Restart))
        {
            Time.timeScale = 1.0f;
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
            // Show end screen
            LevelEnd.Instance.ShowEndScreen(GameTime, false, true);
        }
    }
}
