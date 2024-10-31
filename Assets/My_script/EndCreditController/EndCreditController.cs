using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndCreditController : MonoBehaviour // Control End Credit
{
    [Header("End Credit")]
    [SerializeField] private CanvasGroup winUI;
    [SerializeField] private GraphicRaycaster winUIBlockRaycast;
    [SerializeField] private CanvasGroup endCreditUI;
    [SerializeField] private float fadeinSpeed;
    [SerializeField] private float fadeoutSpeed;
    [SerializeField] private float scrollingSpeed;
    [SerializeField] private bool isEndCreditStart = false;
    [SerializeField] private Scrollbar scrollBarValue;

    [Header("Check Win Condition")]
    [SerializeField] private CheckWinConditionLevel1 checkWinCondition;

    [Header("Music Controller")]
    private bool isPlayEndCreditMusic;
    private MusicController musicController;

    private void Start()
    {
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
    }

    private void Update()
    {
        StartFadeOutWinUIIfWin();
        StartFadeInEndCredit();
    }

    private void StartFadeOutWinUIIfWin() //Start Fade Out Win UI
    {
        if (checkWinCondition.isWin == true && checkWinCondition.IswinUIFullyShow)
        {
            winUI.alpha = winUI.alpha - fadeoutSpeed * Time.deltaTime;

            if (winUI.alpha <= 0.2 && checkWinCondition.isWin == true)
            {
                winUI.alpha = 0f;
                isEndCreditStart = true;
            }
        }   
    }

    private void StartFadeInEndCredit() //Start Fade In End Credit
    {
        if(isEndCreditStart == true && isPlayEndCreditMusic == false)
        {
            musicController.ChangeToTemporalMusic("endCreditMusic");
            isPlayEndCreditMusic = true;
        }

        if(isEndCreditStart == true)
        {
            winUIBlockRaycast.enabled = false; // disable Graphic Raycaster
            endCreditUI.alpha = endCreditUI.alpha + fadeinSpeed * Time.deltaTime;
        }

        if(endCreditUI.alpha >= 1) //if End Credit UI is fully show, it will start scrolling
        {
            StartScrollingEndCredit();
        }
    }

    private void StartScrollingEndCredit()
    {
        if(scrollBarValue.value >= 0)
        scrollBarValue.value = scrollBarValue.value - scrollingSpeed * Time.deltaTime;
    }

}
