using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainmenuUI;
    public GameObject pauseUI;
    public GameObject HUDUI;
    [SerializeField] private string sceneName;

    public Slider loadingSlider;
    public TextMeshProUGUI loadingTitle;
    public GameObject pressanykeyUI;

    public Sprite[] backgrounds;
    public Image backgroundImage;

    public TextMeshProUGUI tipsText;

    public string[] tips;
    public int tipCount;

    public bool doAnimation;
    public Animator transition;
    public float transitionTime = 1f;

    public Sprite[] controlSprites;
    public Image controlImage;
    public int controlCount;

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

        if (transition)
        {
            if (doAnimation)
            {
                StartCoroutine(StartAnimation());
                transition.enabled = true;
                transition.gameObject.GetComponent<CanvasGroup>().enabled = true;
            }
            else
            {
                transition.enabled = false;
                transition.gameObject.GetComponent<CanvasGroup>().enabled = false;
            }
        }

        StartCoroutine(LoadAsynchronously(sceneName));
        StartCoroutine(GenerateTips());
        StartCoroutine(GenerateControls());
    }

    /// <summary>
    /// Disables the UI.
    /// </summary>
    public void DeactivateUIs()
    {
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

    /// <summary>
    /// Starts the scroll animation.
    /// </summary>
    /// <returns></returns>
    IEnumerator StartAnimation()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;

            if (progress >= .9f && !operation.allowSceneActivation)
            {
                pressanykeyUI.SetActive(true);

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


    public IEnumerator GenerateControls()
    {
        controlCount = Random.Range(0, controlSprites.Length);
        controlImage.sprite = controlSprites[controlCount];

        while (loadingScreen.activeInHierarchy)
        {
            yield return new WaitForSeconds(4f);

            controlCount++;
            if (controlCount >= controlSprites.Length)
            {
                controlCount = 0;
            }

            controlImage.sprite = controlSprites[controlCount];
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
    public void BackToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
