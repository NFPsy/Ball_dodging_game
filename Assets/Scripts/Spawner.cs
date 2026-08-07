using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnInterval = 3f;
    public float spawnHeight = 6f;
    public float warningSize = 1.8f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnSphere), spawnInterval, spawnInterval);
    }

    void SpawnSphere()
    {
        float half = (GameManager.GridSize - 1) / 2f;
        int gridX = Random.Range(0, GameManager.GridSize);
        int gridZ = Random.Range(0, GameManager.GridSize);
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
