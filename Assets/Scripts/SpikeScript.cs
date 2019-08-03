using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : LivingBeing
{
    Animator anim;
    public float timer;
    [HideInInspector] public float timerDf;


    void Start()
    {
        timerDf = timer;
        anim = GetComponent<Animator>();
        anim.SetFloat("Spiker", timer);
    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().hasArrow == false)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                anim.SetFloat("Spiker", timer);
            }
            if (timer <= 0)
            {
                anim.SetFloat("Spiker", timerDf);
                timer = timerDf;
            }
        }
    }
}
