using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Lv1To2 : MonoBehaviour
{
    public UnityEvent callFunction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            callFunction.Invoke();
        }
    }
}
