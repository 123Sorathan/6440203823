using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWinConditionLevel1 : MonoBehaviour
{
    public bool isKeyCollect;
    public bool isWin;
    public bool isShowingWinUI;
    public bool IswinUIFullyShow;

    private bool isplayerEnterDoor;

    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject notifyPlayerToFindAKey;
    [SerializeField] private SaveGame saveGame;

    [SerializeField] public CanvasGroup Win_UIGroup;
    [SerializeField] private ParticleSystem Effect;
    public Camera mainCamera;
    private MusicController musicController;

    private PlayerSoundEffectController playerSoundEffectController;

    private void Start()
    {
        HitWinUI();
        Effect.Stop();

        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerSoundEffectController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoundEffectController>();
        }

    }
    private void Update()
    {
        if (Effect != null && mainCamera != null)
        {
            Vector3 centerPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f; // ระยะห่างจากกล้อง
            Effect.transform.position = centerPosition;
        }
    }

    //private void OnTriggerStay2D(Collider2D other) {
    //    if(other.CompareTag("Player") && isKeyCollect == true)
    //    {
    //       //Show Win UI
    //       //winUI.SetActive(true);
    //       Effect.Play();
    //       ShowWinUI();


    //       saveGame.SaveLevel1();
    //       isWin = true;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player") && isKeyCollect == false){
           //Notify player to find a key
           notifyPlayerToFindAKey.SetActive(true);
           StartCoroutine(HideFindKeyNotification());
           playerSoundEffectController.StartPlayEnterDoorSound();//Play dead sound
        }

        else if (other.CompareTag("Player") && isKeyCollect == true)
        {
            //Show Win UI
            //winUI.SetActive(true);
            Effect.Play();
            ShowWinUI();
            musicController.ChangeToTemporalMusic("winMusic"); // play win music

            saveGame.SaveLevel(SceneManager.GetActiveScene().name);
            isWin = true;

            if (!isplayerEnterDoor)
            {
                playerSoundEffectController.StartPlayEnterDoorSound();//Play dead sound
                isplayerEnterDoor = true;
            }
        }
    }

    IEnumerator HideFindKeyNotification()
    {
        yield return new WaitForSeconds(3);
        notifyPlayerToFindAKey.SetActive(false);
    }

    public void HitWinUI()
    {
        Win_UIGroup.alpha = 0f;
        Win_UIGroup.interactable = false;
        Win_UIGroup.blocksRaycasts = false;
        isShowingWinUI = false;
    }

    public void ShowWinUI()
    {
        StartCoroutine(WinUI());
        Win_UIGroup.interactable = true;
        Win_UIGroup.blocksRaycasts = true;
        isShowingWinUI = true;
    }

    IEnumerator WinUI()
    {
        while (Win_UIGroup.alpha < 1f)
        {
            Win_UIGroup.alpha += Time.deltaTime * 1f; 
            yield return null;
        }
        IswinUIFullyShow = true;
        Win_UIGroup.alpha = 1f; 
    }
}
