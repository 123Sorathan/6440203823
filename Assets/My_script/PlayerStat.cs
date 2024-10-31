using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStat : MonoBehaviour // สคริปนี้แสดงสถานะต่างๆ เช่น HP ที่เหลือ, พลังโจมตี, จำนวนครั้งที่ผู้เล่นตาย
{
   public HP_Player Hp;
   
   [SerializeField] public TextMeshProUGUI showHp;
   [SerializeField] public TextMeshProUGUI Countdown;
   [SerializeField] public ReSponse respawn;

   [SerializeField] private Animator anim;
   
   [SerializeField] public Collider2D playerCollider;
   [SerializeField] public PlayerController player;
   public Transform respawnPoint; // ตำแหน่งเริ่มต้นที่ผู้เล่นจะถูกย้ายไป

   [Header("UI Scene Dead")]
   public int  countdeath;
   [SerializeField]  public CanvasGroup deathUIGroup; // ความเร็วในการแสดงผล
   public float respawnDelay = 5f; // เวลานับถอยหลังเมื่อผู้เล่นตาย
   public float fadeSpeed = 1f;
   public bool isDead = true;

   [Header("UI Scene Lose")]
   [SerializeField] public CanvasGroup LoseScene;
   private bool isLose = false;

   [SerializeField] HP_Player hp;
   [SerializeField] private HealthBar healthBar;
   [SerializeField] private GameObject ButtonUI_Upgrade;

   [SerializeField] private ButtonController animBtton;
   [SerializeField] private MusicController musicController;

   private PlayerSoundEffectController playerSoundEffectController;

    void Start()
    {
        // เริ่มต้นให้ UI โปร่งใสทั้งหมด
        deathUIGroup.alpha = 0f;
        HideDeathUI();
        HideLoseUI();
        LoseScene.alpha = 0f;

        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerSoundEffectController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoundEffectController>();
        }
    }

    void Death ()
    {
        if (countdeath == 0)
        {
            PlayerPrefs.DeleteKey("Save Coin");
            anim.SetTrigger("Death");
            HideDeathUI();
            musicController.ChangeToTemporalMusic("loseMusic"); // play lose music
            StopCoroutine(RespawnPlayer());
            ShowLoseUI();
        }
        else if (Hp.currentHealth <= 0 && isDead == true)
        {
            playerSoundEffectController.StartPlayDeadSound();//Play dead sound

            isDead = false; // ตั้ง isDead เป็น false ทันทีหลังจากเข้ามาในเงื่อนไขนี้
            anim.SetTrigger("Death");
            ButtonUI_Upgrade.SetActive(false);
            countdeath--;
            Savecountdeath();
            ShowDeathUI();

            player.rb.velocity = Vector2.zero; // หยุดการเคลื่อนที่ของ Rigidbody
            playerCollider.gameObject.tag = "Untagged"; // เปลี่ยน Tag เป็น "Untagged" 
            player.enabled = false; // ปิดการควบคุมผู้เล่น

            musicController.ChangeToTemporalMusic("loseMusic"); // play lose music

            StartCoroutine(RespawnPlayer()); // เริ่มกระบวนการเกิดใหม่
            Debug.Log("เล่นส่วนนี้ dead");
        }
    }

   void Update() 
   {
        showHp.text = $"x{countdeath}";

        //if (PlayerPrefs.HasKey("Savecountdeath") && PlayerPrefs.GetInt("Savecountdeath") != 0)
        //{
        //    //โชว์ค่าที่เก็บไว้
        //    countdeath = PlayerPrefs.GetInt("Savecountdeath");
        //}
        Death();
   }

    // ฟังก์ชันสำหรับค่อยๆ แสดง UI โดยการเปลี่ยนค่าความโปร่งใส
    IEnumerator FadeInDeathUI()
    {
        while (deathUIGroup.alpha < 1f)
        {
            deathUIGroup.alpha += Time.deltaTime * fadeSpeed; // เพิ่มค่าความโปร่งใสทีละนิด
            yield return null; // รอ frame ต่อไป
        }
        deathUIGroup.alpha = 1f; // ตั้งค่าให้เต็ม 100% เมื่อจบการ fade
    }
    IEnumerator LoseUI()
    {
        while (LoseScene.alpha < 1f)
        {
            LoseScene.alpha += Time.deltaTime * fadeSpeed; 
            yield return null;
        }
        LoseScene.alpha = 1f; 
    }

    IEnumerator RespawnPlayer()
    {
        float countdown = respawnDelay;

        // เริ่มนับถอยหลัง
        while (countdown > 0)
        {
            Countdown.text = $"{countdown}"; // แสดงเวลาที่เหลือเป็นทศนิยม 1 ตำแหน่ง
            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }

        musicController.StartPlayMainMusic();

        // Start monster ignore player
        StartCoroutine(EnemyIgnorePlayer());

        // เมื่อเวลาหมด นำผู้เล่นกลับไปที่จุดเริ่มต้น
        if(countdeath != 0)
        {
            HideDeathUI();
            HideLoseUI();

            animBtton.anim_ButtonUI_stack_upgrade.Rebind();
            anim.ResetTrigger("Death");
            anim.Play("Player");
            deathUIGroup.alpha = 0;
            playerCollider.transform.position = respawnPoint.position;

            // เปิดใช้งานการเคลื่อนไหวอีกครั้ง
            player.enabled = true;

            // รีเซ็ตสถานะผู้เล่น
            isDead = false;

            Hp.currentHealth = Hp.maxHealth;
            healthBar.ResetHealthBar();
            if (countdeath > 0 && isDead == false)
            {
                isDead = true;
            }
        }
    }

    IEnumerator EnemyIgnorePlayer()
    {
        yield return new WaitForSeconds(2f);
        playerCollider.gameObject.tag = "Player";
    }

    void Savecountdeath()
    {
     PlayerPrefs.SetInt("Savecountdeath", countdeath);
     PlayerPrefs.Save();
    }






    // ฟังก์ชันสำหรับซ่อน death UI
    public void HideDeathUI()
    {
        deathUIGroup.alpha = 0f; // ซ่อน UI
        deathUIGroup.interactable = false; // ไม่ให้ UI รับการคลิก
        deathUIGroup.blocksRaycasts = false; // ไม่ให้ UI บล็อกการคลิก
    }

    // ฟังก์ชันสำหรับแสดง death UI
    public void ShowDeathUI()
    {
        StartCoroutine(FadeInDeathUI());
        deathUIGroup.interactable = true; // ให้ UI รับการคลิก
        deathUIGroup.blocksRaycasts = true; // ให้ UI บล็อกการคลิก
    }

    public void HideLoseUI()
    { 
        LoseScene.alpha = 0f;
        LoseScene.interactable = false;
        LoseScene.blocksRaycasts = false;
    }
    public void ShowLoseUI()
    {
        StartCoroutine(LoseUI());
        LoseScene.interactable = true; 
        LoseScene.blocksRaycasts = true; 
    }
}
