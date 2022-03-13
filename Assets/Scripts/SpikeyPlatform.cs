using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeyPlatform : Platform
{
    [SerializeField] private Color32 normalColor = new Color(255, 255, 255);
    [SerializeField] private Color32 damageColor = new Color(255, 153, 153);
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            AddForceUp(collision);
            AddDamage(collision);
        }
        else
        {
            //coming from the bottom
        }
    }

    public void AddDamage(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerManager = collision.gameObject.GetComponent<PlayerManager>();
            playerManager.health -= platformObject.damage;
            playerManager.healthBar.AddRemoveSegments(platformObject.damage);
            var playerMesh = collision.transform.GetChild(0).GetComponent<SpriteRenderer>();
            StartCoroutine(SwitchColor(playerMesh));

        }
    }

    IEnumerator SwitchColor(SpriteRenderer mesh)
    {
        mesh.color = damageColor;
        yield return new WaitForSeconds(0.5f);
        mesh.color = normalColor;
    }

    protected override void AddForceUp(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerManager = collision.gameObject.GetComponent<PlayerManager>();
            if (playerManager.health <= 1)
            {
                Rigidbody2D rigidbody = playerManager.GetComponent<Rigidbody2D>();
                if (rigidbody != null)
                {
                    Vector2 velocity = rigidbody.velocity;
                    velocity.y = platformObject.jumpForce / 4;
                    rigidbody.velocity = velocity;

                }
            }
            else
            {
                base.AddForceUp(collision);
            }
        }
    }
}
