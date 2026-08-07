using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnInterval = 3f;
    public float spawnHeight = 10f;
    public float rangeX = 5f;
    public float rangeZ = 5f;
    public float warningSize = 1.2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnSphere), spawnInterval, spawnInterval);
    }

    void SpawnSphere()
    {
        float x = transform.position.x + Random.Range(-rangeX, rangeX);
        float z = transform.position.z + Random.Range(-rangeZ, rangeZ);

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
