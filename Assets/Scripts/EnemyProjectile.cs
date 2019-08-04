using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : LivingBeing
{

    private Vector3 m_Direction = Vector3.zero;
    private float m_BulletSpeed;
    //[SerializeField] private Sprite m_Sprite;

    private GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        //if (m_Sprite)
        //{
        //    renderer.sprite = m_Sprite;
        //}

        go = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            if (go.GetComponent<PlayerController>().GetComponentInChildren<ShootingScript>())
            {
                if (go.GetComponent<PlayerController>().GetComponentInChildren<ShootingScript>().hasArrow)
                {

                    return;
                }
            }
        }

        Move();
    }

    private void Move()
    {
        if (m_Direction != Vector3.zero)
        {
            transform.position += m_Direction * m_BulletSpeed;
        }
    }

    public void SetDirection(Vector3 _Direction, float _BulletSpeed)
    {
        m_Direction = _Direction;
        m_BulletSpeed = _BulletSpeed;

        if (m_Direction.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void OnHit()
    {
        Destroy(gameObject);
    }
}
