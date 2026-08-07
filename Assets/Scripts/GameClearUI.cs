using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Text))]
public class GameClearUI : MonoBehaviour
{
    private Text clearText;

    void Awake()
    {
        clearText = GetComponent<Text>();
        clearText.text = "";
    }

    void Update()
    {
        if (!GameManager.IsGameClear)
        {
            clearText.text = "";
            return;
        }

        clearText.text = "GAME CLEAR";

        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.anyKey.wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
