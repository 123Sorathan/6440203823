using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.TMP_Text;
using System;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text enemyCounting;
    public GameObject[] shadowSlimeCount;
    public  int sumOfEnemy;
    private int shadowSlimeNo;
    [SerializeField] private GameObject[] ghostCounting;
    private int ghostNo;
    [SerializeField] private GameObject[] SpitterCounting;
    private int spitterNo;
    [SerializeField] private GameObject[] SkeletonCounting;
    private int skeletonNo;

    [SerializeField] private GameObject[] MothCounting;
    private int MothNo;
    [SerializeField] private GameObject[] FireflyCounting;
    private int FireflyNo;
    [SerializeField] private GameObject[] Mystical_swordCounting;
    private int Mystical_swordNo;

    [SerializeField] private GameObject[] BeetleCounting;
    private int BeetleNo;
    [SerializeField] private GameObject[] Giant_WormCounting;
    private int Giant_WormNo;


    private void Start() 
    {
        shadowSlimeCount = GameObject.FindGameObjectsWithTag("ShadowSlime");
        shadowSlimeNo = shadowSlimeCount.Length;

        ghostCounting = GameObject.FindGameObjectsWithTag("The Ghost");
        ghostNo = ghostCounting.Length;

        SpitterCounting = GameObject.FindGameObjectsWithTag("Spitter");
        spitterNo = SpitterCounting.Length;

        // Add code
        SkeletonCounting = GameObject.FindGameObjectsWithTag("Skeleton");
        skeletonNo = SkeletonCounting.Length;

        // Enemy Moth
        MothCounting = GameObject.FindGameObjectsWithTag("Moth");
        MothNo = MothCounting.Length;

        // Enemy Firefly
        FireflyCounting = GameObject.FindGameObjectsWithTag("Firefly");
        FireflyNo = FireflyCounting.Length;

        // Enemy Mystical sword
        Mystical_swordCounting = GameObject.FindGameObjectsWithTag("Mystical sword");
        Mystical_swordNo = Mystical_swordCounting.Length;

        BeetleCounting = GameObject.FindGameObjectsWithTag("Beetle");
        BeetleNo = BeetleCounting.Length;

        Giant_WormCounting = GameObject.FindGameObjectsWithTag("Giant Worm");
        Giant_WormNo = Giant_WormCounting.Length;


        sumOfEnemy = shadowSlimeNo + ghostNo + spitterNo + skeletonNo + MothNo + FireflyNo + Mystical_swordNo + BeetleNo + Giant_WormNo;
        enemyCounting.text = "x " + sumOfEnemy.ToString();
    }

    public void DecreaseEnemyCount(string enemyName)
    {
        if(enemyName == "ShadowSlime")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        } 

        if(enemyName == "The Ghost")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        }

        if (enemyName == "Spitter")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        }
        if (enemyName == "Skeleton")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        }
        if (enemyName == "Moth")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        }
        if (enemyName == "Firefly")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        }
        if (enemyName == "Mystical sword")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        }

        if (enemyName == "Beetle")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        }

        if (enemyName == "Giant Worm")
        {
            sumOfEnemy--;
            enemyCounting.text = "x " + sumOfEnemy.ToString();
        }
    }
}
