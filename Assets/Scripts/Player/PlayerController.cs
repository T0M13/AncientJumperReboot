using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    PlayerManager playerManager;
    Rigidbody2D rbody;
    [SerializeField] private float speed = 750f;
    [SerializeField] private float fallingThreshold = 1f;
    [Range(0, 0.5f)]
    [SerializeField] private float flipOffset = 0.1f;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.Log("GameManager NOT found");
        }
        playerManager = GetComponent<PlayerManager>();
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckFall();
        CheckDirection();
    }

    private void FixedUpdate()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        if (playerManager.canMove)
        {
            Vector3 tilt = new Vector3(Input.acceleration.x, 0.0f, 0.0f);
            Vector2 velocity = rbody.velocity;
            velocity.x = tilt.x * speed * Time.deltaTime;
            rbody.velocity = velocity;
        }
    }

    void CheckFall()
    {
        if (rbody.velocity.y < fallingThreshold)
        {
            playerManager.isFalling = true;
            playerManager.playerMesh.sprite = playerManager.landingSprite;
        }
        else
        {
            playerManager.isFalling = false;
            playerManager.playerMesh.sprite = playerManager.jumpSprite;
        }
    }
    void CheckDirection()
    {
        if (!gameManager.isPaused)
        {
            if (Input.acceleration.x + flipOffset < 0)
            {
                playerManager.playerMesh.flipX = true;
            }
            if (Input.acceleration.x - flipOffset > 0)
            {
                playerManager.playerMesh.flipX = false;
            }
        }


    }
}
