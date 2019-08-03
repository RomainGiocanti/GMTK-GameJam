using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingBeing
{
    private Rigidbody2D rb;
    private Vector2 mv;
    private float defaultSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speed;
    }

    void Update()
    {
        Vector2 mi = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mv = mi.normalized * speed;
        if(GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().hasArrow == true)
        {
            speed = newSpeed;
        } else if (GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().hasArrow == false)
        {
            speed = defaultSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + mv * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }
}
