using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundPointerOnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UISoundController uiSoundController;
    bool isMouseEnter = false;

    private void Start()
    {
        uiSoundController = GameObject.FindGameObjectWithTag("UISounEffect").GetComponent<UISoundController>();    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isMouseEnter == false)
        {
            uiSoundController.MouseOnUISound();
            isMouseEnter = true;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseEnter = false;
    }

    
}
