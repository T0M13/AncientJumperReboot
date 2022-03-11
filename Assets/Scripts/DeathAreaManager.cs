using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAreaManager : MonoBehaviour
{
    public GameManager gameManager;
    public SaveLoadScript saveloadInstance;
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

        SaveData(playerManager);

        yield return new WaitForSeconds(deathTime);

        gameManager.GameOver(playerManager.highscore);
    }

    public void SaveData(PlayerManager playerManager)
    {
        if (saveloadInstance.highscore < playerManager.highscore)
        {
            saveloadInstance.highscore = Mathf.RoundToInt(playerManager.highscore);
            saveloadInstance.coins += playerManager.coins;
            saveloadInstance.SaveSaveData();
        }
        else
        {
            saveloadInstance.coins += playerManager.coins;
            saveloadInstance.SaveSaveData();
        }

    }
}
