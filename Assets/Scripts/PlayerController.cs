using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingBeing
{
    private Rigidbody2D rb;
    private Vector2 mv;
    private float defaultSpeed;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speed;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(life <= 0)
        {
            anim.SetTrigger("Died");
            Destroy(gameObject, 1.5f);
        
        }
        if (transform.GetComponent<Knockback>().knockBack == false)
        {
            Vector2 mi = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            mv = mi.normalized * speed;

            #region Flip And Animations
            if (mi != Vector2.zero)
            {
                anim.SetFloat("Moving", 1);
            }
            else
            {
                anim.SetFloat("Moving", 0);
            }
            if(mi.x == -1)
            {
                transform.GetComponent<SpriteRenderer>().flipX = true;
            } 
            if(mi.x == 1)
            {
                transform.GetComponent<SpriteRenderer>().flipX = false;
            }
            #endregion

            if (GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().hasArrow == true)
            {
                speed = newSpeed;
            }
            else if (GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().hasArrow == false)
            {
                speed = defaultSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        if (transform.GetComponent<Knockback>().knockBack == false)
            rb.MovePosition(rb.position + mv * Time.fixedDeltaTime);
    }

}
