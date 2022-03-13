using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformScriptableObject platformObject;
    protected LevelGenerator levelGenerator;

    protected virtual void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        if (!levelGenerator)
        {
            Debug.LogWarning("LevelGenerator NOT found");
        }
    }

    protected virtual void Update()
    {
        //Debug.DrawLine(gameObject.transform.position, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - platformObject.despawnRange), Color.red);

        if ((gameObject.transform.position.y < Camera.main.transform.position.y - platformObject.despawnRange))
        {
            if (platformObject.platformType == PlatformScriptableObject.PlatformTypes.baseground)
            {
                Destroy(gameObject);
            }
            else
            {
                //levelGenerator.RepositionPlatform(this.gameObject);
                levelGenerator.DespawnPlatform(this.gameObject);
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            AddForceUp(collision);
        }
        else
        {
            //coming from the bottom
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerManager>().isFalling = false;
    }

    protected virtual void AddForceUp(Collision2D collision)
    {
        Rigidbody2D rigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            Vector2 velocity = rigidbody.velocity;
            velocity.y = platformObject.jumpForce;
            rigidbody.velocity = velocity;

        }
    }

}


