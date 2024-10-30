using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] healthBarImages;
    public Slider slider;
    public Gradient gradient;
    public HP_Player hp;
    
    // public Image fill;
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        UpdateHealthImages(health);

        // fill.color = gradient.Evaluate(0.1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        UpdateHealthImages(health);

        // fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // �ѧ��ѹ�ѻവ�Ҿ��ʹ���ʹ���������ʹ�Ѩ�غѹ
    private void UpdateHealthImages(int currentHealth)
    {
        // �ӹǳ�����繵�ͧ���ʹ������������
        float healthPercentage = (float)currentHealth / hp.maxHealth * 100;

        // ��͹�ء�Ҿ��͹ ¡����Ҿ�á����ʴ��֧���ʹ�����ʹ
        for (int i = 1; i < healthBarImages.Length; i++)
        {
            healthBarImages[i].enabled = false;
        }

        // �ʴ��Ҿ����дѺ���ʹ�Ѩ�غѹ
        if (healthPercentage > 75)
        {
            healthBarImages[0].enabled = true; // �ʴ��Ҿ��� 1 (���ʹ�����ʹ)
        }
        else if (healthPercentage > 50)
        {
            healthBarImages[1].enabled = true; // �ʴ��Ҿ��� 2 (���ʹ�ҡ���� 50%)
        }
        else if (healthPercentage > 25)
        {
            healthBarImages[2].enabled = true; // �ʴ��Ҿ��� 3 (���ʹ�ҡ���� 25%)
        }
        else if (healthPercentage > 0)
        {
            healthBarImages[3].enabled = true; // �ʴ��Ҿ��� 4 (���ʹ�ҡ���� 0%)
        }
        else
        {
            healthBarImages[4].enabled = true; // �ʴ��Ҿ��� 5 (�����ʹ)
        }
    }

    public void ResetHealthBar()
    {
        // ��͹�ء�Ҿ¡����Ҿ�á
        for (int i = 1; i < healthBarImages.Length; i++)
        {
            healthBarImages[i].enabled = false;
        }
        healthBarImages[0].enabled = true; // �ʴ��Ҿ��� 1 (���ʹ�����ʹ)
    }
}
