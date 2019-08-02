using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public float slowDown = 5f;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0f;
    }
    private void Update()
    {
        slowDown -= Time.deltaTime;
        if(slowDown <= 0)
        {
            speed = 0f;
            rb.velocity = transform.right * speed;
        } else if (slowDown <= 1)
        {
            speed = 1f;
            rb.velocity = transform.right;
        } else if (slowDown <= 2)
        {
            speed = 2f;
            rb.velocity = transform.right / speed;
        } else if (slowDown <= 3)
        {
            speed = 3f;
            rb.velocity = transform.right / speed;
        } else if (slowDown <= 4)
        {
            speed = 4f;
            rb.velocity = transform.right / speed;
        }
    }
}
