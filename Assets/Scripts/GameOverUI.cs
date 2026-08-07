using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameOverUI : MonoBehaviour
{
    private Text gameOverText;

    void Awake()
    {
        gameOverText = GetComponent<Text>();
        gameOverText.text = "";
    }

    void Update()
    {
        gameOverText.text = GameManager.IsGameOver ? "GAME OVER" : "";
    }
}
