using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        if (!GameManager.IsGameOver)
        {
            gameOverText.text = "";
            return;
        }

        gameOverText.text = "GAME OVER\n다시 시작하시겠습니까? (Y/N)";

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.yKey.wasPressedThisFrame)
        {
            GameManager.IsGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (keyboard.nKey.wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
