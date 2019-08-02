using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing : MonoBehaviour
{
    [Header("Life Basics:")]
    public int life;
    [Range(0, 10)]
    public float speed;
    [Range(0, 3)]
    public int damage;
    
}
