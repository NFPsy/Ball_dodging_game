using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnHeight = 6f;
    public float warningSize = 1.8f;

    private const float BaseInterval = 3f;
    private const float MinInterval = 1.5f;
    private const float IntervalStep = 0.75f;
    private const float FinalInterval = 1f;
    private const float PhaseDuration = 20f;
    private const int MaxBalls = 10;
    private const float ClearHoldTime = 30f;

    private static readonly int MaxPhase1Steps = Mathf.RoundToInt((BaseInterval - MinInterval) / IntervalStep);

    private float gameTime = 0f;
    private float spawnTimer = 0f;

    void Update()
    {
        if (GameManager.IsGameOver || GameManager.IsGameClear || GameManager.IsPaused) return;

        gameTime += Time.deltaTime;

        int phase1Steps = Mathf.Min(MaxPhase1Steps, Mathf.FloorToInt(gameTime / PhaseDuration));
        float spawnInterval;
        int ballsPerSpawn;

        if (phase1Steps < MaxPhase1Steps)
        {
            spawnInterval = BaseInterval - IntervalStep * phase1Steps;
            ballsPerSpawn = 1;
        }
        else
        {
            spawnInterval = MinInterval;
            float phase2Time = gameTime - PhaseDuration * MaxPhase1Steps;
            int phase2Steps = Mathf.Min(MaxBalls - 1, Mathf.FloorToInt(phase2Time / PhaseDuration));
            ballsPerSpawn = 1 + phase2Steps;

            if (ballsPerSpawn >= MaxBalls)
            {
                spawnInterval = FinalInterval;
                float phase3Time = phase2Time - PhaseDuration * (MaxBalls - 1);
                if (phase3Time >= ClearHoldTime)
                {
                    GameManager.IsGameClear = true;
                    return;
                }
            }
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnBalls(ballsPerSpawn);
        }
    }

    void SpawnBalls(int count)
    {
        int totalCells = GameManager.GridSize * GameManager.GridSize;
        count = Mathf.Min(count, totalCells);

        int[] cells = new int[totalCells];
        for (int i = 0; i < totalCells; i++) cells[i] = i;
        for (int i = totalCells - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = cells[i];
            cells[i] = cells[j];
            cells[j] = temp;
        }

        for (int i = 0; i < count; i++)
        {
            int gridX = cells[i] % GameManager.GridSize;
            int gridZ = cells[i] / GameManager.GridSize;
            SpawnSphere(gridX, gridZ);
        }
    }

    void SpawnSphere(int gridX, int gridZ)
    {
        float half = (GameManager.GridSize - 1) / 2f;
        float x = transform.position.x + (gridX - half) * GameManager.CellSize;
        float z = transform.position.z + (gridZ - half) * GameManager.CellSize;

        float fallDistance = spawnHeight - transform.position.y;
        float fallDuration = Mathf.Sqrt(2f * fallDistance / Mathf.Abs(Physics.gravity.y));
        SpawnWarningMarker(x, z, fallDuration);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(x, spawnHeight, z);
        sphere.tag = "Ball";
        sphere.AddComponent<Rigidbody>();
        sphere.AddComponent<Ball>();
    }

    void SpawnWarningMarker(float x, float z, float lifetime)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(marker.GetComponent<Collider>());
        marker.transform.position = new Vector3(x, 0.01f, z);
        marker.transform.localScale = new Vector3(warningSize, 0.01f, warningSize);
        marker.GetComponent<Renderer>().material.color = Color.red;
        Destroy(marker, lifetime);
    }
}
