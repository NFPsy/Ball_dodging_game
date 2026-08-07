using UnityEngine;

public class Ball : MonoBehaviour
{
    public float destroyDelay = 0.05f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
