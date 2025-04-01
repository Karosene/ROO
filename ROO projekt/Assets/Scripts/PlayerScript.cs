using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for reloading the scene
using TMPro; // Required for UI text (optional)

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    private float[] lanes = { -3f, 0f, 3f };
    private int currentLane = 1;
    private float jumpingDuration = .5f;
    private bool isJumping = false;

    private Rigidbody2D rb;
    private Collider2D playerCollider;

    public Sprite jumpingSprite;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private TMP_Text _text;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        originalSprite = spriteRenderer.sprite;

        Time.timeScale = 1; // Ensure time is normal when restarting
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0 && !isJumping)
        {
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLane < lanes.Length - 1 && !isJumping)
        {
            currentLane++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            playerCollider.enabled = false;
            StartCoroutine(PlayerJumping());
        }

        Vector3 targetPosition = new Vector3(lanes[currentLane], transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    IEnumerator PlayerJumping()
    {
        spriteRenderer.sprite = jumpingSprite;
        playerCollider.enabled = false;

        rb.velocity = new Vector2(rb.velocity.x, 8f);

        yield return new WaitForSeconds(jumpingDuration);

        rb.velocity = new Vector2(0, -8f);

        spriteRenderer.sprite = originalSprite;
        playerCollider.enabled = true;

        isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Obstacle")) 
        {
            Debug.Log("Collided with an obstacle! Game Over.");
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        TimerScript.StopTimer();
    }
}
