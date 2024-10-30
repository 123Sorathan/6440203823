using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
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


    private bool isOnGround ;
    private bool canDoubleJump;

    private static int HurtTriggerAnimatorHash = Animator.StringToHash("HurtTrigger");
    private static int IsPlayerRunAnimatorHash = Animator.StringToHash("IsPlayerRun");
    private static int IsPlayerFloatAnimatorHash = Animator.StringToHash("IsPlayerFloat");

    // private static int IsPlayerAnimatorHash = Animator.StringToHash("");
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     animator.SetTrigger(HurtTriggerAnimatorHash );
        // }
        // if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        // {
        //     rb2d.velocity = Vector2.zero;
        //     return;
        // }



        float moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rb2d.velocity = new Vector2(moveHorizontal, rb2d.velocity.y);
        Debug.Log(moveHorizontal);

        if(moveHorizontal != 0){

            spriteRenderer.flipX = moveHorizontal < 0;

        }  

        animator.SetBool(IsPlayerRunAnimatorHash, Mathf.Abs(moveHorizontal) > 0 && isOnGround);
        animator.SetBool(IsPlayerFloatAnimatorHash, !isOnGround);

        if(isOnGround)
        {
            canDoubleJump = true;
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
    }

    private void Jump()
    {
         rb2d.velocity = new Vector2(rb2d.velocity.x, JumpVelocity);
    }

    private void FixedUpdate()
    {
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
}
