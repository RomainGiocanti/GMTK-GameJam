using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public float slowDown = 5f;
    public float decreasingValue;
    public float timeToStartSlow = 2f;
    bool slowingDown = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0f;
    }
    private void Update()
    {
        ArrowSpeed();
    }

    void ArrowSpeed()
    {
        if (!slowingDown)
        {
            rb.velocity = transform.right * speed;

            timeToStartSlow -= Time.deltaTime;
        }

        if (timeToStartSlow <= 0)
        {
            slowingDown = true;
        }

        if (slowingDown)
        {
            if (speed > 0)
            {
                rb.velocity = transform.right * speed;

                speed -= decreasingValue;

                print(speed);
            }

            else if(speed <= 0)
            {
                rb.velocity = Vector2.zero;
            }
        }

    }
}
