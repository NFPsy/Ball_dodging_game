using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SurvivalTimer : MonoBehaviour
{
    private Text timerText;
    private float elapsedTime = 0f;

    void Awake()
    {
        timerText = GetComponent<Text>();
    }

    void Update()
    {
        if (!GameManager.IsGameOver && !GameManager.IsGameClear)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            GameManager.UpdateBestTime(elapsedTime);
        }
        timerText.text = Mathf.FloorToInt(elapsedTime) + "s";
    }
}
