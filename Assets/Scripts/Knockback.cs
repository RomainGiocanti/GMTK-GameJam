using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb;
    public bool knockBack = false;
    public float force;
    private Vector2 knockDirection;
    [Range(0, 0.25f)]
    public float knocktime;
    float knockTimeDf;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        knockTimeDf = knocktime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") ||
             collision.gameObject.CompareTag("eHit"))
        {
            knockDirection = (collision.transform.position - transform.position).normalized;
            knockBack = true;
        }
    }

    private void Update()
    {
        
        if (knockBack == true)
        {
            rb.velocity = -knockDirection * force;
            knocktime -= Time.deltaTime;
        }
        if (knocktime <= 0)
        {
            rb.velocity = Vector2.zero;
            knockBack = false;
            knocktime = knockTimeDf;
        }
    }
}
