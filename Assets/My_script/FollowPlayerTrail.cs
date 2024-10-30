using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerTrail : MonoBehaviour
{
    public Transform playerTransform;
    private TrailRenderer trailRenderer;
    [SerializeField] private Vector3 foot;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // ตำแหน่ง Trail Renderer ตามตำแหน่งของ Player
            transform.position = playerTransform.position;
        }
        
    }
}
