using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider;  
    public TextMeshProUGUI loadingText;    
    public TextMeshProUGUI percentageText;
    public string sceneName;

    //[SerializeField] private PlayerStat Stat;

    void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
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
}

// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using System.Collections;
// using TMPro;

// public class LoadingScreen : MonoBehaviour
// {
//     public Slider loadingSlider;  
//     public TextMeshProUGUI loadingText;    
//     public TextMeshProUGUI percentageText;   

//     void Start()
//     {
//         StartCoroutine(LoadYourAsyncScene());
//     }

//     IEnumerator LoadYourAsyncScene()
//     {
//         // Start loading the Scene
//         AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level_1");
//         asyncOperation.allowSceneActivation = false;

//         while (!asyncOperation.isDone)
//         {
//             // Get the progress, which is a float from 0 to 0.9
//             float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

//             // Update the UI elements
//             loadingSlider.value = progress;
//             percentageText.text = (progress * 100f).ToString("F0") + "%";

//             // Optional: you can display loading messages
//             loadingText.text = "Loading...";

//             // Allow the scene to activate when it's finished
//             if (asyncOperation.progress >= 0.9f)
//             {
//                 loadingText.text = "Press any key to continue...";
//                 if (Input.anyKeyDown)
//                 {
//                     asyncOperation.allowSceneActivation = true;
//                 }
//             }

//             yield return null;
//         }
//     }
// }


