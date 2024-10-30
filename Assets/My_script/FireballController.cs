using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private HP_Player playerHP;
    private bool isHitPLayer;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private CircleCollider2D fireballCollider;
    private SpriteRenderer fireballSprite;
    private float speed;



    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<HP_Player>();
            startPosition = gameObject.transform.position;
            endPosition = playerHP.transform.position;
            speed = 1f;
        }
        fireballCollider = GetComponent<CircleCollider2D>();
        fireballSprite = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        FireballMove();
        PlayerWasHit();
        Debug.Log("Start POs : " + startPosition);
        Debug.Log("End POs : " + endPosition);

    }
    private void FireballMove()
   {
        if(playerHP != null)
        {
            // Face to Player
            Vector3 directionOfPlayer = /*playerHP.gameObject.transform.position*/ endPosition - startPosition;
            if (directionOfPlayer.x > 0)// Face Right
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (directionOfPlayer.x < 0)// Face Right
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            //move to player
            //transform.position = Vector3.MoveTowards(startPosition, endPosition, speed * Time.deltaTime);
            transform.position += new Vector3(directionOfPlayer.x, 0, 0) * speed * Time.deltaTime;
        }
    }

    private void PlayerWasHit()
    {
        if(playerHP != null)
        {
            if (isHitPLayer == true)
            {
                playerHP.isPoison = true;
                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isHitPLayer = true;
            playerHP.roundOfAttack = 0;
        }
    }
}
