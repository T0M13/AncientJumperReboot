using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game")]
    public Transform hudCanvas;
    public TextMeshProUGUI highscoreUI;
    public Transform gameOverCanvas;
    public TextMeshProUGUI gameOverHighscore;
    public Transform pauseCanvas;

    [Header("Main Menu")]
    public TextMeshProUGUI mainMenuHighscore;

}
