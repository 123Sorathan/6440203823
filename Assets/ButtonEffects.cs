using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ButtonEffects : MonoBehaviour
{

    public ParticleSystem particle_System; // Particle System
    public TMP_Text textMeshProText; // Text Mesh Pro Text

    public void ChangeTextColorToBlack()
    {
        if (textMeshProText != null)
        {
            textMeshProText.color = Color.black; // เปลี่ยนสีของข้อความเป็นสีดำ
        }
    }

    public void PlayParticleEffect()
    {
        if (particle_System != null)
        {
            particle_System.Play(); // เล่น Particle System
        }
    }
}
