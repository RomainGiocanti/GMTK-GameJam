﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public float slowDown = 5f;
    public float lower = 2f;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0f;
        Debug.Log("Hit");
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().hasArrow = true;
            Debug.Log("Hit");
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
            speed = 0f;
            rb.velocity = transform.right * speed;
        } else
        {
            rb.velocity = transform.right / lower;
        }
        Debug.Log(rb.velocity + "");
    }
}
