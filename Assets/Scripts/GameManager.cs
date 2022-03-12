using UnityEngine;
public class GameManager : MonoBehaviour
{
    [ContextMenuItem("Restart Level", "RestartLevel")]
    public UIManager uiManager;
    public SaveLoadScript saveloadInstance;

    public float screenHeight;
    public float screenWidth;
    public bool inMainMenu;

    private void Awake()
    {
        Time.timeScale = 1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        uiManager = FindObjectOfType<UIManager>();
        if (!uiManager)
        {
            Debug.Log("UIManager NOT found");
        }
        else
        {
            if (uiManager.gameOverCanvas)
            {
                uiManager.gameOverCanvas.gameObject.SetActive(false);
            }
        }

        screenWidth = GetScreenWidth(screenWidth);
        screenHeight = GetScreenHeight(screenHeight);
    }

    /// <summary>
    /// Stops the game - Game Over
    /// </summary>
    public void GameOver(float _highscore)
    {
        //Time.timeScale = 0;
        uiManager.gameOverCanvas.gameObject.SetActive(true);
        uiManager.hudCanvas.gameObject.SetActive(false);
        uiManager.gameOverHighscore.text = "Highscore: " + Mathf.RoundToInt(_highscore).ToString() + "m";
    }

    /// <summary>
    /// Get Screen Width - in World Scale though
    /// </summary>
    /// <param name="width"></param>
    /// <returns></returns>
    public float GetScreenWidth(float width)
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        width = edgeVector.x * 2;
        return width;

    }
    /// <summary>
    /// Get Screen Height - in World Scale though
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public float GetScreenHeight(float height)
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        height = edgeVector.y * 2;
        return height;

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
