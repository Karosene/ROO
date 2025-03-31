using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float fallSpeed = 5f; // Speed of obstacles

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Destroy the obstacle when it goes out of view
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
