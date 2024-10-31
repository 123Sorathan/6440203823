using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CheckProgression : MonoBehaviour
{
    [Header("Level Buttons")]
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button level4Button;

    [Header("Load Scene Component")]
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI percentageText;

    [Header("Notification")]
    [SerializeField] private CanvasGroup notificationAlpha;
    [SerializeField] private float fadeOutSpeed;
    [SerializeField] private bool isFadeFinished;

    [SerializeField] private MusicController musicController;
    private UISoundController uISoundController;


    private void Start()
    {
        uISoundController = GameObject.FindGameObjectWithTag("UISounEffect").GetComponent<UISoundController>();

    }

    public void CheckProgressLevel1()
    {
        if(PlayerPrefs.HasKey("Level_1"))
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadYourAsyncScene("Level_2"));
            uISoundController.ClickSound();
        }
        else
        {
            //Show Notification UI
            notificationAlpha.alpha = 1;
            isFadeFinished = true;
            uISoundController.WarningSound();
        }
    }

    public void CheckProgressLevel2()
    {
        if (PlayerPrefs.HasKey("Level_2"))
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadYourAsyncScene("Level_3"));
            uISoundController.ClickSound();
        }
        else
        {
            //Show Notification UI
            notificationAlpha.alpha = 1;
            isFadeFinished = true;
            uISoundController.WarningSound();
        }
    }

    public void CheckProgressLevel3()
    {
        if (PlayerPrefs.HasKey("Level_3"))
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadYourAsyncScene("Level_4"));
            uISoundController.ClickSound();
        }
        else
        {
            //Show Notification UI
            notificationAlpha.alpha = 1;
            isFadeFinished = true;
            uISoundController.WarningSound();
        }
    }

    public void EnterLevel1(string levelName)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadYourAsyncScene(levelName));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        // Start loading the Scene using the scene name from Inspector
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Get the progress, which is a float from 0 to 0.9
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            // Update the UI elements
            loadingSlider.value = progress;
            percentageText.text = (progress * 100f).ToString("F0") + "%";

            // Display loading messages
            loadingText.text = "Loading...";

            // Allow the scene to activate when it's finished
            if (asyncOperation.progress >= 0.9f)
            {
                loadingText.text = "Loading complete!";
                Time.timeScale = 1;
                //Stat.countdeath = 5;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void Update()
    {
        if(isFadeFinished == true)
        {
            notificationAlpha.alpha -= fadeOutSpeed * Time.deltaTime;
            if(notificationAlpha.alpha <= 0)
            {
                isFadeFinished = false;
                notificationAlpha.alpha = 0;
            }
        }
    }
}
