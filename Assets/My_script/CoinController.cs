using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    public ParticleSystem coinCollectEffect; // ลาก Particle System เข้ามาใน Inspector
    //[SerializeField] TextMeshProUGUI coinText; 
    [SerializeField] private CoinCount countCoin;
    
    private void Awake() {
        //coinText = GameObject.FindGameObjectWithTag("Coin Text").GetComponent<TextMeshProUGUI>();
        countCoin = GameObject.FindGameObjectWithTag("Player").GetComponent<CoinCount>();   
    } 
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            countCoin.coinCount++;
            PlayCoinCollectEffectAndDestroy(); 
            Destroy(gameObject);
        }
    }

    void PlayCoinCollectEffectAndDestroy()
    {
        if (coinCollectEffect != null)
        {
            // เล่น Particle System ที่ตำแหน่งของ "Coin"
            coinCollectEffect.transform.position = transform.position;
            coinCollectEffect.Play();
        }
        else
        {
            Debug.LogWarning("Particle System not assigned in the Inspector.");
        }
    }
}