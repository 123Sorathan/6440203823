using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_ShadowSlime : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private EnemyStat enemyStat;
    public StatOfUnit statOfUnit;
    [SerializeField] private ShadowSlimeController _shadowSlimeController;
    [SerializeField] private PlayerController _playerController;


    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        // ���� EnemyStat �������� ShadowSlime
        enemyStat = GetComponentInParent<EnemyStat>();

        // ��駤�� Max Health �ͧ ShadowSlime ������ HP � EnemyStat
        SetMaxHealth_EnemyShadowSlime();
    }

    private void Update()
    {
        //isHitEnemy();
    }

    public void SetMaxHealth_EnemyShadowSlime()
    {
        if (enemyStat != null)
        {
            slider.maxValue = statOfUnit.unitStats[0].hp;
            slider.value = statOfUnit.unitStats[0].hp;
            fill.color = gradient.Evaluate(1f); // �������������;�ѧ���Ե���
        }
    }

    // �ѧ��ѹ����Ѻ�ѻവ��� Health

    public void isHitEnemy()
    {
        SetHealth(enemyStat.hp);
        //Debug.Log("EnemyHp = = " + enemyStat.hp);
    }



    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
