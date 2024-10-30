using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevScene : MonoBehaviour
{
    void Start()
    {
        Movement_player2D player = FindObjectOfType<Movement_player2D>();

        if (player != null)
        {
        }
        else
        {
            Debug.LogError("Movement_player2D not found in the scene.");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
