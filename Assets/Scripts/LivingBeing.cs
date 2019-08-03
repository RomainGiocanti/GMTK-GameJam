using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing : MonoBehaviour
{
    [Header("Life Basics:")]
    public UnityType unity;
    public int life;
    [Range(0, 10)]
    public float speed;
    [Range(0, 10)]
    public float newSpeed;
    [Range(0, 3)]
    public int damage;

    public enum UnityType //This is the type of the unity
    {
        player, enemy, barrel, obj
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (unity == UnityType.player && collision.gameObject.CompareTag("Enemy") ||
            unity == UnityType.player && collision.gameObject.CompareTag("eHit")
            )
        {
            life -= collision.gameObject.GetComponent<LivingBeing>().damage;
        }
        else if
          (unity == UnityType.enemy && collision.gameObject.CompareTag("Arrow"))
        {
            life -= GameObject.FindGameObjectWithTag("Player").GetComponent<LivingBeing>().damage;
        }
        else if
          (unity == UnityType.barrel && collision.gameObject.CompareTag("Arrow"))
        {
            Destroy(gameObject, 1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (unity == UnityType.player && collision.gameObject.CompareTag("Spike"))
        {
            
            life -= collision.gameObject.GetComponent<LivingBeing>().damage;
        }
    }
}
