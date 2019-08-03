using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    Animator anim;
    public float timer;
    float timerDf;


    void Start()
    {
        timerDf = timer;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
            anim.SetFloat("Spiker", timer);
        }
        if(timer <= 0)
        {
            anim.SetFloat("Spiker", timerDf);
            timer = timerDf;
        }
    }
}
