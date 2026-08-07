using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnInterval = 3f;
    public float spawnHeight = 10f;
    public float rangeX = 5f;
    public float rangeZ = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnSphere), spawnInterval, spawnInterval);
    }

    void SpawnSphere()
    {
        float x = transform.position.x + Random.Range(-rangeX, rangeX);
        float z = transform.position.z + Random.Range(-rangeZ, rangeZ);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(x, spawnHeight, z);
        sphere.tag = "Ball";
        sphere.AddComponent<Rigidbody>();
    }
}
