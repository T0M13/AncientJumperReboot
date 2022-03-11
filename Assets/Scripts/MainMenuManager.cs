using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private SaveLoadScript saveloadInstance;
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (!uiManager)
        {
            Debug.Log("UIManager NOT found");
        }
        else
        {
            if (!saveloadInstance)
            {
                Debug.LogWarning("Save Load Script Missing");
            }
            else
            {
                uiManager.mainMenuHighscore.text = "Highscore: " + saveloadInstance.highscore + "m";
            }
        }
    }

    public void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}