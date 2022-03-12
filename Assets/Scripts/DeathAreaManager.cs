using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAreaManager : MonoBehaviour
{
    public GameManager gameManager;
    public float deathTime = 1f;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.Log("GameManager NOT found");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SetGameOver(collision));
        }
    }

    IEnumerator SetGameOver(Collider2D collision)
    {

        var playerManager = collision.GetComponent<PlayerManager>();
        playerManager.canMove = false;

        gameManager.SaveData(playerManager);

        yield return new WaitForSeconds(deathTime);

        gameManager.GameOver(playerManager.highscore);
    }


}
