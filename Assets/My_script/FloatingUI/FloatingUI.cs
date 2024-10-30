using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public Transform targetFloatingUI;
    public Vector3 uiOffPosition;
    public Vector3 pos;

    private void Update()
    {
        if(targetFloatingUI != null)
        {
            pos = Camera.main.WorldToScreenPoint(targetFloatingUI.position + uiOffPosition);
        }

        if(transform.position != pos)
        {
            transform.position = pos;
        }
    }
}
