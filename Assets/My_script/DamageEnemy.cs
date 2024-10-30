using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public string name;
    public int damage = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    HP_Player player = other.GetComponent<HP_Player>();
        //    if (player != null)
        //    {
        //        player.TakeDamage(damage);
        //    }
        //}
    }
}