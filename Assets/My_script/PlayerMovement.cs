using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private float speed = 8f;
    private float jumpinPower = 10f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator = this.gameObject.GetComponent<Animator>();


        if (Input.GetButtonDown("Jump") && IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, jumpinPower);
            animator.SetTrigger("Jump");

        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

          float speedValue = Mathf.Abs(horizontal);
          animator.SetFloat("Speed", speedValue);

        Flip();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
      return Physics2D.OverlapCircle(groundCheck.position, 0.5f,groundLayer);
    }

    private void Flip(){
         if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    }
}
