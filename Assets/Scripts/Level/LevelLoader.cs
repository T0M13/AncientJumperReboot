using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public GameObject loadingScreen;
    public GameObject mainmenuUI;
    public GameObject deathScreenUI;
    public GameObject pauseUI;
    public GameObject HUDUI;

    public Slider loadingSlider;
    public TextMeshProUGUI loadingTitle;
    public GameObject pressanykeyUI;

    public Sprite[] backgrounds;
    public Image backgroundImage;

    public TextMeshProUGUI tipsText;

    public string[] tips;
    public int tipCount;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
    }

    public void LoadLevel(string sceneName)
    {
        Time.timeScale = 1;

        backgroundImage.sprite = backgrounds[Random.Range(0, backgrounds.Length)];

        DeactivateUIs();

        loadingScreen.SetActive(true);
        loadingTitle.text = sceneName;

        StartCoroutine(LoadAsynchronously(sceneName));
        StartCoroutine(GenerateTips());
    }

    /// <summary>
    /// Disables the UI.
    /// </summary>
    public void DeactivateUIs()
    {
        if (deathScreenUI)
        {
            deathScreenUI.SetActive(false);
        }

        if (mainmenuUI)
        {
            mainmenuUI.SetActive(false);
        }

        if (pauseUI)
        {
            pauseUI.SetActive(false);
        }

        if (HUDUI)
        {
            HUDUI.SetActive(false);
        }
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingSlider.gameObject.SetActive(true);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;

            if (progress >= .9f && !operation.allowSceneActivation)
            {
                pressanykeyUI.SetActive(true);
                loadingSlider.gameObject.SetActive(false);

                if (Input.anyKey)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    public IEnumerator GenerateTips()
    {
        tipCount = Random.Range(0, tips.Length);
        tipsText.text = tips[tipCount];

        //Debug.Log(loadingScreen.activeInHierarchy);

        while (loadingScreen.activeInHierarchy)
        {

            yield return new WaitForSeconds(8f);

            tipCount++;
            if (tipCount >= tips.Length)
            {
                tipCount = 0;
            }

            tipsText.text = tips[tipCount];
        }
    }

    /// <summary>
    /// Restarts the Level
    /// </summary>
    [ContextMenu("Restart Level")]
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
