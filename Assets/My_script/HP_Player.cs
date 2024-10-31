using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP_Player : MonoBehaviour
{
    // [SerializeField] private Text damageText; 
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBer;
    public int armor;

    public Animator playerAnimator;
    public ParticleSystem hitEffectParent;
    public ParticleSystem hitEffectchild;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private float lastTime = 0f;
    public int roundOfAttack = 0;
    [SerializeField] private StatOfUnit statOfUnit;
    public bool isPoison;
    [SerializeField] private CheckWinConditionLevel1 checkWinCondition;
    private PlayerSoundEffectController playerSoundEffectController;

    // [SerializeField] GameObject panelToShow;
    // public Camera playerCamera;

    void Start()
    {
        currentHealth = maxHealth;
        healthBer.SetMaxHealth(maxHealth);
        checkWinCondition = GameObject.FindGameObjectWithTag("CheckWInCondition").GetComponent<CheckWinConditionLevel1>();

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerSoundEffectController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoundEffectController>();
        }

        hitEffectParent.Pause();
        hitEffectchild.Pause();
    }
    void Update()
    {
        // เช็คว่าผู้เล่นอยู่บนพื้นหรือไม่
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        PlayerPoisonStatus();
    }

    public void TakeDamage(int damage)
    {
        if(checkWinCondition.isWin == false)
        {
            playerSoundEffectController.StartPlayEnemyHitPlayerSound(); // play enemy hit player sound

            currentHealth = currentHealth - (damage - armor);

            healthBer.SetHealth(currentHealth);


            playerAnimator.SetTrigger("Hit");
            if (isGrounded)
            {
                hitEffectParent.Play();
            }
            else { hitEffectchild.Play(); }


            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
    }

    private void PlayerPoisonStatus()
    {
        if(isPoison == true)
        {
            if (Time.time > lastTime + 1f)
            {
                //Count Damage 
                currentHealth = currentHealth - statOfUnit.unitStats[2].attackPower / 2;
                healthBer.SetHealth(currentHealth);
                roundOfAttack++;
                lastTime = Time.time;
            }

            if (roundOfAttack == 2)
            {
                roundOfAttack = 0;
                isPoison = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Enemy"))
        //{
        //    DamageEnemy enemy = other.GetComponent<DamageEnemy>();
        //    if (enemy != null)
        //    {
        //        TakeDamage(enemy.damage);
        //    }
        //}
    }

    // void ShowPanelOnCenter()
    // {
    //     Vector3 centerPosition = playerCamera.WorldToScreenPoint(playerCamera.transform.position);
    //     panelToShow.transform.position = centerPosition;
    //     panelToShow.SetActive(true);
    // }

}