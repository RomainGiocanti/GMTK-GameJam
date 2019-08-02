using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBeing : LivingBeing
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Arrow"))
        {
            life--;
            Destroy(gameObject, 1.5f);
        }
    }

}
