using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float fallSpeed = 4f; 

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -8f)
        {
            Destroy(gameObject);
        }
    }
}
