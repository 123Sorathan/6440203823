
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float recoilLength;
    [SerializeField] float recoilFactor;
    [SerializeField] bool isRecoiling = false;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float patrolDistance = 5f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float aggroRange = 10f;
    [SerializeField] float chaseSpeed = 8f;
    [SerializeField] Animator animator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2 startPos;
    private Vector2 patrolEndPos;
    private float patrolTimer;
    private bool isChasing = false;
    private Vector2 lastMovementDirection;
    

    float recoilTimer;
    Rigidbody2D rb;
    
    public ParticleSystem explosionEffect;
    [SerializeField] Transform groundCheck; // จุดสำหรับตรวจสอบพื้นอยู่ใต้ศัตรู
    [SerializeField] float groundCheckRadius = 0.2f; // รัศมีสำหรับการตรวจสอบ
    [SerializeField] LayerMask groundLayer; // LayerMask สำหรับการตรวจสอบว่าเป็นพื้นหรือไม่
    bool isGrounded;

    public float patrolSpeed = 2.0f; // ความเร็วในการเดินไปมา
    private bool movingRight = true; // ทิศทางเริ่มต้นในการเคลื่อนที่
    public GameObject coinPrefab; // เลือก prefab ของเหรียญที่สร้างไว้ใน Unity Editor

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        patrolEndPos = new Vector2(startPos.x + patrolDistance, startPos.y);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Flip();
        if (health <= 0)
        {
            Destroy(gameObject);
            // Instantiate(coinPrefab, transform.position, Quaternion.identity); 
            Vector3 coinPosition = transform.position + new Vector3(0, 0.5f, 0);  // ตำแหน่งที่เหรียญจะถูกสร้างขึ้น
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);           // สร้างเหรียญ
            return;
        }

        if (isRecoiling)
        {
            RecoilBehavior();
        }
        else
        {
            PlayerDetection();
            if (!isChasing)
            {
                Patrol();
            }
        }

        if (isChasing || Mathf.Abs(rb.velocity.x) > 0.1f) // Check if moving
        {
            animator.SetBool("isMoving", true);
            // Check direction
            if (lastMovementDirection.x < transform.position.x)
            {
                // Moving right
                animator.SetFloat("MoveX", 1f);
            }
            else if (lastMovementDirection.x > transform.position.x)
            {
                // Moving left
                animator.SetFloat("MoveX", -1f);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        lastMovementDirection = transform.position; // Update last position after checking

    }

    void Patrol()
    {
    
        animator.SetBool("isMoving", false);
    }

    void PlayerDetection()
    {

        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, aggroRange, playerLayer);
        if (hitPlayer != null)
        {
            isChasing = true;
            ChasePlayer(hitPlayer.transform.position);
        }
        else
        {
            isChasing = false;
        }
    }

    void ChasePlayer(Vector2 playerPosition)
    {
        if (isChasing)
        {
            Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
            rb.velocity = direction * chaseSpeed;
        }
    }

    void RecoilBehavior()
    {
        if (recoilTimer < recoilLength)
        {
            recoilTimer += Time.deltaTime;
        }
        else
        {
            isRecoiling = false;
            recoilTimer = 0;
            rb.velocity = Vector2.zero; // Stop movement after recoil
        }
    }

    public void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damageDone;
        if (!isRecoiling)
        {
            rb.AddForce(-_hitForce * recoilFactor * _hitDirection);
            isRecoiling = true; // Start recoil behavior
            //animator.SetBool("isExploding", true);
            if (explosionEffect != null)
            {

            explosionEffect.Play();

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // To visualize the aggro range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    void Flip()
    {
        // Check player's position and flip sprite if needed
        if (playerTransform.position.x > transform.position.x)
        {
            // หันภาพเป็นทางขวา
            spriteRenderer.flipX = false;
        }
        else
        {
           // หันภาพเป็นทางซ้าย
           spriteRenderer.flipX = true;
        }
    }
}


