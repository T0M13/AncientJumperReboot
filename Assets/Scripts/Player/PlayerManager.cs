using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseCharacterManager
{
    public float highscore;
    public int coins;
    private GameManager gameManager;
    private UIManager uiManager;

    [SerializeField] private float bounds;
    [SerializeField] private Vector3 position;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.Log("GameManager NOT found");
        }
        uiManager = FindObjectOfType<UIManager>();
        if (!uiManager)
        {
            Debug.Log("UIManager NOT found");
        }

        canMove = true;
        bounds = (gameManager.GetScreenWidth(gameManager.screenWidth) / 2);

    }

    private void Update()
    {
        CountHighScore();
        CheckBounds();
        UpdateUI();
        CheckHealth();
    }

    /// <summary>
    /// Counts the highscore at runtime
    /// </summary>
    public void CountHighScore()
    {
        highscore = Mathf.Max(highscore, transform.position.y);
    }

    /// <summary>
    /// Checks if th player is out of bounds
    /// </summary>
    void CheckBounds()
    {
        if (transform.position.x > bounds)
        {
            position = transform.position;
            position.x = -bounds;
            transform.position = position;
        }
        if (transform.position.x < -bounds)
        {
            position = transform.position;
            position.x = bounds;
            transform.position = position;
        }
    }

    /// <summary>
    /// Updates the UI
    /// </summary>
    void UpdateUI()
    {
        if (uiManager.highscoreUI)
        {
            uiManager.highscoreUI.text = Mathf.RoundToInt(highscore).ToString() + "m";
        }
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            gameManager.GameOver(highscore);
        }
    }

}
