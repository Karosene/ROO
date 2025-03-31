using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    private float[] lanes = { -3f, 0f, 3f }; // 3 lanes
    private int currentLane = 1; // Start in the middle lane

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLane < lanes.Length - 1)
        {
            currentLane++;
        }

        // Move the player to the selected lane smoothly
        Vector3 targetPosition = new Vector3(lanes[currentLane], transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
