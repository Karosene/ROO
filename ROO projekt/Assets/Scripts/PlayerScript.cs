using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for reloading the scene
using TMPro;
using UnityEngine.Rendering.Universal.Internal; // Required for UI text (optional)

public class PlayerScript : MonoBehaviour
{
    private MainManagerScript mainManager;

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
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        originalSprite = spriteRenderer.sprite;

        mainManager = FindObjectOfType<MainManagerScript>();

        if (mainManager == null)
        {
            Debug.LogError("MainManagerScript not found in the scene!");
        }

        Time.timeScale = 1; 
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        mainManager.GameOver();
    }
}
