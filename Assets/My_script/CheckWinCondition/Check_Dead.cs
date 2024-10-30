using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Dead : MonoBehaviour
{
    [SerializeField] private HP_Player playerHP;
    private ShadowSlimeController shadowSlime;
    private SpitterController spitter;
    private SkeletonController skeleton;
    private GhostController ghost;
    private BeetleController beetle;
    private MothController moth;
    private MysticalSwordController mysticalSword;
    private FireflyController firefly;
    void OnTriggerEnter2D(Collider2D coll)
    {     
        if(coll.gameObject.tag == "Player")
        {
           playerHP.currentHealth = 0;

           Debug.Log("Player has triggered an enemy. HP is now 0.");
        }

        else if(coll.gameObject.tag == "ShadowSlime")
        {
            shadowSlime = coll.GetComponentInChildren<ShadowSlimeController>();
            shadowSlime.enemyStat.hp = 0;
        }

        else if(coll.gameObject.tag == "Spitter")
        {
            spitter = coll.GetComponentInChildren<SpitterController>();
            spitter.enemyStat.hp = 0;
        }

        else if (coll.gameObject.tag == "Skeleton")
        {
            skeleton = coll.GetComponentInChildren<SkeletonController>();
            skeleton.enemyStat.hp = 0;
        }

        else if (coll.gameObject.tag == "The Ghost")
        {
            ghost = coll.GetComponentInChildren<GhostController>();
            ghost.enemyStat.hp = 0;
        }

        else if (coll.gameObject.tag == "Beetle")
        {
            beetle = coll.GetComponentInChildren<BeetleController>();
            beetle.enemyStat.hp = 0;
        }

        else if (coll.gameObject.tag == "Moth")
        {
            moth = coll.GetComponentInChildren<MothController>();
            moth.enemyStat.hp = 0;
        }

        else if (coll.gameObject.tag == "Mystical Sword")
        {
            mysticalSword = coll.GetComponentInChildren<MysticalSwordController>();
            mysticalSword.enemyStat.hp = 0;
        }

        else if (coll.gameObject.tag == "Firefly")
        {
            firefly = coll.GetComponentInChildren<FireflyController>();
            firefly.enemyStat.hp = 0;
        }
    }
}
