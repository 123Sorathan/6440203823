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

    // ฟังก์ชันอัปเดตภาพหลอดเลือดตามค่าเลือดปัจจุบัน
    private void UpdateHealthImages(int currentHealth)
    {
        // คำนวณเปอร์เซ็นต์ของเลือดที่เหลืออยู่
        float healthPercentage = (float)currentHealth / hp.maxHealth * 100;

        // ซ่อนทุกภาพก่อน ยกเว้นภาพแรกที่แสดงถึงเลือดเต็มหลอด
        for (int i = 1; i < healthBarImages.Length; i++)
        {
            healthBarImages[i].enabled = false;
        }

        // แสดงภาพตามระดับเลือดปัจจุบัน
        if (healthPercentage > 75)
        {
            healthBarImages[0].enabled = true; // แสดงภาพที่ 1 (เลือดเต็มหลอด)
        }
        else if (healthPercentage > 50)
        {
            healthBarImages[1].enabled = true; // แสดงภาพที่ 2 (เลือดมากกว่า 50%)
        }
        else if (healthPercentage > 25)
        {
            healthBarImages[2].enabled = true; // แสดงภาพที่ 3 (เลือดมากกว่า 25%)
        }
        else if (healthPercentage > 0)
        {
            healthBarImages[3].enabled = true; // แสดงภาพที่ 4 (เลือดมากกว่า 0%)
        }
        else
        {
            healthBarImages[4].enabled = true; // แสดงภาพที่ 5 (หมดหลอด)
        }
    }

    public void ResetHealthBar()
    {
        // ซ่อนทุกภาพยกเว้นภาพแรก
        for (int i = 1; i < healthBarImages.Length; i++)
        {
            healthBarImages[i].enabled = false;
        }
        healthBarImages[0].enabled = true; // แสดงภาพที่ 1 (เลือดเต็มหลอด)
    }
}
