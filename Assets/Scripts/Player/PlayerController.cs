using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    PlayerManager playerManager;
    Rigidbody2D rbody;
    [SerializeField] private float speed = 750f;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckFall();
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
        if (playerManager.highscore > transform.position.y)
        {
            playerManager.isFalling = true;
        }
        else
        {
            playerManager.isFalling = false;
        }
    }
}
