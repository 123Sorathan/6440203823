using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControler : MonoBehaviour
{
        public float dampTime = 1.0f;
        public Vector3 velocity = Vector2.zero;
        public Transform target;

        Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target){
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f,0.3f,point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position,destination,
            ref velocity, dampTime
            );
        }
    }
}
