using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int gridX;
    private int gridZ;

    void Start()
    {
        gridX = GameManager.GridSize / 2;
        gridZ = GameManager.GridSize / 2;
        ApplyGridPosition();
    }

    void Update()
    {
        if (GameManager.IsGameOver || GameManager.IsGameClear) return;

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        int newX = gridX;
        int newZ = gridZ;
        if (keyboard.aKey.wasPressedThisFrame) newX -= 1;
        if (keyboard.dKey.wasPressedThisFrame) newX += 1;
        if (keyboard.sKey.wasPressedThisFrame) newZ -= 1;
        if (keyboard.wKey.wasPressedThisFrame) newZ += 1;

        newX = Mathf.Clamp(newX, 0, GameManager.GridSize - 1);
        newZ = Mathf.Clamp(newZ, 0, GameManager.GridSize - 1);

        if (newX != gridX || newZ != gridZ)
        {
            gridX = newX;
            gridZ = newZ;
            ApplyGridPosition();
        }
    }

    void ApplyGridPosition()
    {
        float half = (GameManager.GridSize - 1) / 2f;
        float x = (gridX - half) * GameManager.CellSize;
        float z = (gridZ - half) * GameManager.CellSize;
        transform.position = new Vector3(x, transform.position.y, z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.IsGameOver = true;
            Debug.Log("Game Over");
        }
    }
}
