using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement Settings")]
    public Rigidbody2D rb;
    [SerializeField] public float walkSpeed;
    [SerializeField] private float xAxis, yAxis;

    [Header("Vertical Movement Settings")]
    [SerializeField] private float JumpForce;
    private int jumpBufferCounter = 0;
    [SerializeField] private int jumpBufferFrames;
    private float coyoteTimeCounter = 0;
    [SerializeField] private float coyoteTime;
    private int airJumpCounter = 0;
    [SerializeField] public int maxAirJumps;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] GameObject dashEffect;
    [SerializeField] GameObject dashAirEffect;

    [Header("Attacking")]
    bool attack = false;
    float timeBetweenAttack, timeSinceAttck;
    [SerializeField] Transform SideAttackTransform, UpAttackTransform, DownAttackTransform;
    [SerializeField] Vector2 SideAttackArea, UpAttackArea, DownAttackArea;
    [SerializeField] LayerMask attackableLayer;
    [SerializeField] GameObject slashEffect;
    public int damage;


    [Header("Recoil")]
    [SerializeField] int recoilXSteps = 5;
    [SerializeField] int recoilYSteps = 5;
    [SerializeField] float recoilXSpeed = 100;
    [SerializeField] float recoilYSpeed = 100;
    int stepsXRecoiled, stepsYRecoiled;



    public static PlayerController Instance;
    PlayerStateList pState;
    Animator anim;
    private bool canDash = true;
    private bool dashed;
    private float gravty;

    private bool isCoolDownAttack = false;




    private void Awake()
    {
        // Debug.Log(canDash);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pState = GetComponent<PlayerStateList>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravty = rb.gravityScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SideAttackTransform.position, SideAttackArea);
        Gizmos.DrawWireCube(UpAttackTransform.position, UpAttackArea);
        Gizmos.DrawWireCube(DownAttackTransform.position, DownAttackArea);
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        UpdateJumpVariables();
        if (pState.dashing) return;
        Flip();
        Move();
        Jump();
        StartDash();
        Attack();
        // if(Input.Get)
        // EnemyHealthCalculation();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        attack = Input.GetMouseButtonDown(0);
    }

    private void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        anim.SetBool("Run", rb.velocity.x != 0 && Grounded());
    }

    void StartDash()
    {
        if (Input.GetButtonDown("Dash") && canDash && !dashed)
        {
            StartCoroutine(Dash());
            dashed = true;
        }

        if (Grounded())
        {
            dashed = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        pState.dashing = true;
        anim.SetTrigger("Dash");
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        if (Grounded()) { Instantiate(dashEffect, transform); }
        else if (!Grounded()) Instantiate(dashAirEffect, transform);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravty;
        pState.dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }

    void Attack()
    {
        timeSinceAttck += Time.deltaTime;
        if (attack && timeSinceAttck >= timeBetweenAttack && isCoolDownAttack == false)
        {
            timeSinceAttck = 0;
            anim.SetTrigger("Attack_1");
            /*if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                if (yAxis == 0 || yAxis < 0 && Grounded())
                {
                    Hit(SideAttackTransform, SideAttackArea, ref pState.recoilingX, recoilXSpeed);
                    Instantiate(slashEffect, SideAttackTransform);
                }
                else if (yAxis > 0)
                {
                    Hit(UpAttackTransform, UpAttackArea, ref pState.recoilingY, recoilYSpeed);
                    SlashEffectAtAngle(slashEffect, 90, UpAttackTransform);
                }
                else if (yAxis < 0 && !Grounded())
                {
                    Hit(DownAttackTransform, DownAttackArea, ref pState.recoilingY, recoilYSpeed);
                    SlashEffectAtAngle(slashEffect, -90, DownAttackTransform);
                }
                isCoolDownAttack = true;
                StartCoroutine(Attaack_Cooldown());
            }*/

                if (!isCoolDownAttack)
                {
                    PerformAttack();
                    isCoolDownAttack = true; // µÑé§¤èÒãËéËÂØ´¡ÒÃâ¨ÁµÕ
                    StartCoroutine(Attaack_Cooldown()); // àÃÔèÁ¤ÙÅ´ÒÇ¹ì
                }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"))
        {
            /*if (attack && timeSinceAttck >= timeBetweenAttack && isCoolDownAttack == false)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                {
                    if (!isCoolDownAttack)
                    {
                        PerformAttack();
                        isCoolDownAttack = true; // µÑé§¤èÒãËéËÂØ´¡ÒÃâ¨ÁµÕ
                        StartCoroutine(Attaack_Cooldown()); // àÃÔèÁ¤ÙÅ´ÒÇ¹ì
                    }
                }
            }*/

            
        }

    }

    private void PerformAttack()
    {
        if (yAxis == 0 || (yAxis < 0 && Grounded()))
        {
            Hit(SideAttackTransform, SideAttackArea, ref pState.recoilingX, recoilXSpeed);
            Instantiate(slashEffect, SideAttackTransform);
        }
        else if (yAxis > 0)
        {
            Hit(UpAttackTransform, UpAttackArea, ref pState.recoilingY, recoilYSpeed);
            SlashEffectAtAngle(slashEffect, 90, UpAttackTransform);
        }
        else if (yAxis < 0 && !Grounded())
        {
            Hit(DownAttackTransform, DownAttackArea, ref pState.recoilingY, recoilYSpeed);
            SlashEffectAtAngle(slashEffect, -90, DownAttackTransform);
        }
    }

    IEnumerator Attaack_Cooldown()
    {
      yield return new WaitForSeconds(0.02f);
      isCoolDownAttack = false;
    }

    private void Hit(Transform _attackTransform, Vector2 _attackArea, ref bool _recoilDir, float _recoilStrength)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0, attackableLayer);

        // Debug.Log(objectsToHit.Length);

        if(objectsToHit.Length > 0)
        {
            // Debug.Log("Hit");
            _recoilDir = true;
        }
        for(int i = 0; i < objectsToHit.Length; i++)
        {
            if (objectsToHit[i].GetComponent<EnemyStat>() != null)
            {
                objectsToHit[i].GetComponent<EnemyStat>().hp -= damage;
                if (objectsToHit[i].CompareTag("ShadowSlime"))
                {
                    ShadowSlimeController ShadowSlimeController = objectsToHit[i].GetComponentInChildren<ShadowSlimeController>();
                    ShadowSlimeController.isHit = true;
                }
                if (objectsToHit[i].CompareTag("The Ghost"))
                {
                    GhostController GhostController = objectsToHit[i].GetComponentInChildren<GhostController>();
                    GhostController.isHit = true;
                }
                if (objectsToHit[i].CompareTag("Spitter"))
                {
                    SpitterController SpitterController = objectsToHit[i].GetComponentInChildren<SpitterController>();
                    SpitterController.isHit = true;
                }
                if (objectsToHit[i].CompareTag("Skeleton"))
                {
                    SkeletonController SkeletonController = objectsToHit[i].GetComponentInChildren<SkeletonController>();
                    SkeletonController.isHit = true;
                }
                if (objectsToHit[i].CompareTag("Moth"))
                {
                    MothController MothController = objectsToHit[i].GetComponentInChildren<MothController>();
                    MothController.isHit = true;
                }
                if (objectsToHit[i].CompareTag("Firefly"))
                {
                    FireflyController FireflyController = objectsToHit[i].GetComponentInChildren<FireflyController>();
                    FireflyController.isHit = true;
                }
                if (objectsToHit[i].CompareTag("Mystical sword"))
                {
                    MysticalSwordController MysticalSwordController = objectsToHit[i].GetComponentInChildren<MysticalSwordController> ();
                    MysticalSwordController.isHit = true;
                }
                if (objectsToHit[i].CompareTag("Beetle"))
                {
                    BeetleController BeetleController = objectsToHit[i].GetComponentInChildren<BeetleController>();
                    BeetleController.isHit = true;
                }
                if (objectsToHit[i].CompareTag("Giant Worm"))
                {
                    GiantWormController GiantWormController = objectsToHit[i].GetComponentInChildren< GiantWormController > ();
                    GiantWormController.isHit = true;
                }
                Rigidbody2D enemyRB = objectsToHit[i].GetComponent<Rigidbody2D>();
                enemyRB.AddForce((objectsToHit[i].transform.position - gameObject.transform.position).normalized * 8, ForceMode2D.Impulse); //
            }
        }
    }
    void SlashEffectAtAngle(GameObject _slashEffect, int _effectAngle, Transform _attackTransform)
    {
        _slashEffect = Instantiate(_slashEffect, _attackTransform);
        _slashEffect.transform.eulerAngles = new Vector3(0, 0, _effectAngle);
        _slashEffect.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }
    void Recoil()
    {
        if(pState.recoilingX)
        {
            if(pState.lookingRight)
            {
                rb.velocity = new Vector2 (-recoilXSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2 (recoilXSpeed, 0);
            }
        }

        if(pState.recoilingY)
        {
            rb.gravityScale = 0;
            if(yAxis < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, recoilYSpeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -recoilYSpeed);
            }
            airJumpCounter = 0;
        }
        else
        {
            rb.gravityScale = gravty;
        }

        //stop recoil
        if(pState.recoilingX && stepsXRecoiled < recoilXSteps)
        {
            stepsXRecoiled++;
        }
        else
        {
            StopRecoilX();
        }
        if(pState.recoilingX && stepsYRecoiled < recoilYSteps)
        {
            stepsYRecoiled++;
        }
        else
        {
            StopRecoilY();
        }

        if(Grounded())
        {
            StopRecoilY();
        }
    }
    void StopRecoilX()
    {
        stepsXRecoiled = 0; 
        pState.recoilingX = false;

    }
     void StopRecoilY()
    {
        stepsXRecoiled = 0; 
        pState.recoilingY = false;
        
    }

    public bool Grounded()
    {
        if(Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround)
           || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
           || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Flip()
    {
        if(xAxis > 0)
        {
            transform.localScale = new Vector2(1,transform.localScale.y);
            pState.lookingRight = true;
        }
        else if (xAxis < 0)
        {
            transform.localScale = new Vector2(-1,transform.localScale.y);
             pState.lookingRight = false;
        }
    }

    void Jump()
    {
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            pState.jumping = false;
        }
        if(!pState.jumping)
        {
            if(jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {

                rb.velocity = new Vector3(rb.velocity.x, JumpForce);

                pState.jumping = true;
            }
            else if(!Grounded() && airJumpCounter < maxAirJumps && Input.GetButtonDown("Jump"))
            {
                pState.jumping = true;
                airJumpCounter++;
                rb.velocity =  new Vector3(rb.velocity.x, JumpForce);
            }
        }
         if(Input.GetButtonDown("Jump") && Grounded())
         {

            rb.velocity = new Vector3(rb.velocity.x, JumpForce);

            pState.jumping = true;
         }
         anim.SetBool("Jump", !Grounded());
    }

    void UpdateJumpVariables()
    {
        if (Grounded())
        {
            pState.jumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpCounter = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferFrames;
        }
        else
        {
            jumpBufferCounter--;
        }

    }

    private void EnemyHealthCalculation()
    {
        Collider2D[] colliderOfEnemy = Physics2D.OverlapBoxAll(SideAttackTransform.position, SideAttackArea, 0 , attackableLayer);
        Debug.Log("Enemy = " + colliderOfEnemy.Length);
        if(colliderOfEnemy.Length == 0)
        {
          // Do nothing
        }

        else if (colliderOfEnemy.Length > 0){
            for(int i = 0 ; i < colliderOfEnemy.Length; i++)
            {
                int healthOfEnemy = colliderOfEnemy[i].GetComponent<EnemyStat>().hp;
                colliderOfEnemy[i].GetComponent<EnemyStat>().hp = healthOfEnemy - damage;
            }

        }

    }


}

   
