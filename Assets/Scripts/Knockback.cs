using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool knockBack = false;
    public float force;
    private Vector2 knockDirection;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D> ();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("If failed");
        if (collision.gameObject.CompareTag("Enemy") ||
             collision.gameObject.CompareTag("eHit"))
        {
                        knockDirection = (collision.transform.position - transform.position).normalized;
            Debug.Log("Collision");
        }
    }
    private void Update()
    {
        if (knockBack == true)
        {
            rb.AddForce(-knockDirection * force, ForceMode2D.Impulse);
            knockBack = false;
        }
    }
}
