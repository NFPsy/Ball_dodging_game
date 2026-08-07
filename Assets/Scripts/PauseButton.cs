using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Text label;

    void Awake()
    {
        label = GetComponentInChildren<Text>();
        GetComponent<Button>().onClick.AddListener(TogglePause);
    }

    void TogglePause()
    {
        GameManager.IsPaused = !GameManager.IsPaused;
        Time.timeScale = GameManager.IsPaused ? 0f : 1f;

        if (label != null)
        {
            label.text = GameManager.IsPaused ? "Resume" : "Pause";
        }
    }
}
