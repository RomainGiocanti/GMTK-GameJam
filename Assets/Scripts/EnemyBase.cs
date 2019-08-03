using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// maybe inherit from an upper "base" class
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class EnemyBase : LivingBeing
{
    [SerializeField] protected float m_MoveSpeed = 0.01f;
    [SerializeField] protected float m_ChaseSpeed = 0.02f;
    [SerializeField] protected int m_MaxLife = 3;
    [SerializeField] protected int m_CurrentLife = 3;
    [SerializeField] protected int m_AttackDamages = 1;
    [SerializeField] protected Sprite m_Sprite;
    [SerializeField] protected float m_TimeToDestroy = 1.5f;

    [SerializeField] protected GameObject m_DebugPlayer;

    [SerializeField] protected float m_ReloadTime;
    private float m_TimerReloading = 0;

    protected bool m_IsDead = false;

    [SerializeField] protected float m_AttackRange;

    [SerializeField] protected List<Transform> m_PatrolPoints;

    private int m_IndexPatrolling = 0;

    protected Vector3 m_DebugPlaceToGo = Vector3.zero;
    protected GameObject go;

    protected ContactFilter2D m_ContactFilter;

    // Start is called before the first frame update
    protected void Start()
    {
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        if (m_Sprite)
        {
            renderer.sprite = m_Sprite;
        }

        go = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected void Update()
    {
        if (go && go.GetComponent<PlayerController>() && go.GetComponent<PlayerController>().GetComponent<ShootingScript>() && go.GetComponent<PlayerController>().GetComponent<ShootingScript>().hasArrow)
        {
            return;
        }
        // move
        // try attack
        // check if dead
        if (!m_IsDead)
        {
            TryMove();
            TryAttack();
            //CheckIfDead();
        }

    }

    protected virtual void TryMove()
    {
        if (InRange())
        {
            ChasePlayer();
        }
        else
        {
            Patrolling();
        }

        ObstacleAvoidance();
        // perform steering behavior
    }


    protected virtual void ObstacleAvoidance()
    {

    }

    protected virtual void ChasePlayer()
    {
        m_DebugPlaceToGo = m_DebugPlayer.transform.position;
        Vector3 direction = m_DebugPlayer.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * m_ChaseSpeed;
    }


    // maybe if we just too far from patrol points , it comes back to patrolling
    protected virtual void Patrolling()
    {
        if (CloseToGoal(m_PatrolPoints[m_IndexPatrolling].position))
        {
            m_IndexPatrolling++;
            if (m_IndexPatrolling >= m_PatrolPoints.Count)
            {
                m_IndexPatrolling = 0;
            }
        }

        m_DebugPlaceToGo = m_PatrolPoints[m_IndexPatrolling].position;

        Vector3 directionToGo = m_PatrolPoints[m_IndexPatrolling].position - transform.position;
        directionToGo.Normalize();

        transform.position += directionToGo * m_MoveSpeed;
    }


    protected bool CloseToGoal(Vector3 _Goal)
    {
        return Vector3.Distance(transform.position, _Goal) < 0.01;
    }

    //not useful now
    private int FindRandomPlaceToGo()
    {
        int index = Random.Range(0, m_PatrolPoints.Count - 1);
        return 0;
    }

    protected virtual void TryAttack()
    {
        if (InRange())
        {
            if (CoolDownGood())
            {
                Attack();
                m_TimerReloading = 0;
            }
        }

        if (!CoolDownGood())
        {
            m_TimerReloading += Time.deltaTime;
        }

    }

    protected bool InRange()
    {
        return Vector3.Distance(m_DebugPlayer.transform.position, gameObject.transform.position) < m_AttackRange;
    }

    protected bool CoolDownGood()
    {
        return m_TimerReloading >= m_ReloadTime;
    }

    public void TakeDamages(int _AmountDamages)
    {
        m_CurrentLife -= _AmountDamages;

        if (m_CurrentLife <= 0)
        {
            Destroy(gameObject, m_TimeToDestroy);
            m_IsDead = true;
            // is there is a collider , disable it
        }
    }

    protected virtual void Attack()
    {

    }

    private void OnDrawGizmos()
    {
        // draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);

        if (m_DebugPlaceToGo != Vector3.zero)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, m_DebugPlaceToGo);
        }

    }
}
