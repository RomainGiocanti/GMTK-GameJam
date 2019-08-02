using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public float lifetime;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * speed;
        Invoke("DestroyProjectile", lifetime);
    }

    private void OnTriggerEnter2D(Collider2D Hitinfo)
    {

        Destroy(gameObject);
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
