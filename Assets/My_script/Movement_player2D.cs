using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Movement_player2D : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;

    [SerializeField] private float moveSpeed;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private float JumpVelocity;
    [SerializeField] private Vector3 footOffset;
    [SerializeField] private float footRadius;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float fallMultiplier;


    //playerDash

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 16f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private int coinCount = 0;
    private int coin = 1;
    private int maxCoinCount;
    public TextMeshProUGUI CoinText;
    
    


    [SerializeField] private TrailRenderer tr;


    private bool isOnGround ;
    private bool canDoubleJump;

    private static int HurtTriggerAnimatorHash = Animator.StringToHash("HurtTrigger");
    private static int IsPlayerRunAnimatorHash = Animator.StringToHash("IsPlayerRun");
    private static int IsPlayerFloatAnimatorHash = Animator.StringToHash("IsPlayerFloat");
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = "COIN: "+coinCount+" / "+maxCoinCount;
       
        float moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rb2d.velocity = new Vector2(moveHorizontal, rb2d.velocity.y);
        // Debug.Log(moveHorizontal);

        if(moveHorizontal != 0){

            spriteRenderer.flipX = moveHorizontal < 0;

        }  

        animator.SetBool(IsPlayerRunAnimatorHash, Mathf.Abs(moveHorizontal) > 0 && isOnGround);
        animator.SetBool(IsPlayerFloatAnimatorHash, !isOnGround);

        if(isOnGround)
        {
            canDoubleJump = true;
        }

        if(isDashing)
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(isOnGround){
                Jump();
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        if(Input.GetMouseButtonDown(1) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

       private void Jump()
    {
         rb2d.velocity = new Vector2(rb2d.velocity.x, JumpVelocity);
    }

    private void FixedUpdate()
    {
        if(isDashing){
            return;
        }


       Collider2D hitCollider =  Physics2D.OverlapCircle(transform.position + footOffset, footRadius, groundLayerMask);
       isOnGround = hitCollider != null;


       if(rb2d.velocity.y<0)
       {
        rb2d.velocity += Physics2D.gravity.y * fallMultiplier * Vector2.up * Time.deltaTime;

       }
    }

    private void OnDrawGizmos() {
        Gizmos.color = isOnGround ? Color.green : Color.red;
        Gizmos.color = canDoubleJump && !isOnGround ? Color.blue : Gizmos.color;
        Gizmos.DrawWireSphere(transform.position + footOffset, footRadius);
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
         yield return new WaitForSeconds(dashingCooldown);
         canDash = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin"){
            coinCount += 1;
        }
    }
}


    

