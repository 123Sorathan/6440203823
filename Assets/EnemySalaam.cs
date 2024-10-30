using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySalaam : MonoBehaviour
{
    public float walkSpeed = 1.0f;
    public float walkLeft,walkRight = 0.5f;
    public float walkDirection = 0.5f;
    public GameObject Explode;
    Vector3 WalkAmount;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
         WalkAmount.x = (walkDirection = walkSpeed) * Time.deltaTime;
         if(walkDirection > 0.0f && transform.position.x >= walkRight){
            walkDirection =-1.0f;
        }
        else if (walkDirection < 0.0f && transform.position.x <= walkLeft)
        {
            walkDirection = 1.0f;
        }
        transform.Translate(WalkAmount);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Attackable"){
            Destroy(other.gameObject);
            StartCoroutine(secondDeath(0.2f));
        }
    }

    IEnumerator secondDeath(float sec){
        yield return new WaitForSeconds(sec);
        Instantiate(Explode,transform.position,transform.rotation);
        Destroy(this.gameObject);
    }
}
