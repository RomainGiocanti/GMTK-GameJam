using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public float slowDown = 5f;
    public float decreaseValue;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0f;
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().hasArrow = true;
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().buttonHold =
                GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().buttonHoldDefault;
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (slowDown >= 0)
        {
            slowDown -= Time.deltaTime;
        }
        if(slowDown <= 0)
        {
            if (speed >= 0)
            {
                speed -= decreaseValue;
            }

            if (speed < 0)
            {
                speed = 0;
            }
            rb.velocity = transform.right * speed;
        }
    }
}
