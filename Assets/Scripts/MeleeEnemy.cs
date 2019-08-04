using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    [SerializeField] bool m_IsAngry;
    private float m_BasicMoveSpeed;
    private bool m_HasCharged = false;
    private bool m_EndCharge = false;
    private RaycastHit2D[] m_Hited = new RaycastHit2D[16];
    [SerializeField] private float m_MeleeAttackRange = 0.5f;

    //----------------------------

    //obstacke avoidance

    [SerializeField] protected List<GameObject> m_Obstacles;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        m_BasicMoveSpeed = m_MoveSpeed;
        m_ContactFilter.SetLayerMask(LayerMask.NameToLayer("Player"));
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        //soi trop loin ou arrivé au cac / la charge est finie
        if (CloseToGoal(m_DebugPlayer.transform.position) || Vector3.Distance(m_DebugPlayer.transform.position, transform.position) > m_AttackRange)
        {
            if (m_HasCharged)
            {
                m_EndCharge = true;
            }

            if (Vector3.Distance(m_DebugPlayer.transform.position, transform.position) > m_AttackRange)
            {
                m_EndCharge = false;
            }
        }
    }

    protected override void TryMove()
    {
        base.TryMove();
    }

    protected override void TryAttack()
    {
        base.TryAttack();
    }

    protected override void Attack()
    {
        if (m_IsAngry)
        {
            AngryZombieAttack();
        }
        else
        {
            ClassicZombieAttack();
        }
    }

    private void ClassicZombieAttack()
    {
        if (InMeleeRange())
        {
            Debug.Log("MeleeAttack");
        }
    }

    private void AngryZombieAttack()
    {
        if (!m_HasCharged)
        {
            Charge();
        }

        if (InMeleeRange())
        {
            Debug.Log("MeleeAttack");
        }
    }

    private bool InMeleeRange()
    {
        return Vector3.Distance(transform.position, m_DebugPlayer.transform.position) < m_MeleeAttackRange;
    }

    private void Charge()
    {
        // charge speed
        //m_MoveSpeed = 0.1f;
        m_HasCharged = true;
    }

    protected override void ChasePlayer()
    {
        if (!m_IsAngry)
        {
            base.ChasePlayer();
        }
        else
        {
            m_DebugPlaceToGo = m_DebugPlayer.transform.position;
            if (!m_EndCharge)
            {
                Debug.Log("RUSHHHH");
                Vector3 direction = m_DebugPlayer.transform.position - transform.position;
                direction.Normalize();
                transform.position += direction * 0.1f;
            }
            else
            {
                Debug.Log("chase");
                Vector3 direction = m_DebugPlayer.transform.position - transform.position;
                direction.Normalize();
                transform.position += direction * m_ChaseSpeed;
            }
        }

    }

    private GameObject GetClosestObstacle()
    {
        if (m_Obstacles.Count == 0)
        {
            return null;
        }

        float distance = Mathf.Infinity;
        GameObject objectToReturn = null;

        foreach (GameObject obstacle in m_Obstacles)
        {
            if (Vector3.Distance(obstacle.transform.position, transform.position) < distance)
            {
                objectToReturn = obstacle;
            }
        }

        return objectToReturn;
    }

    protected override void ObstacleAvoidance()
    {
        GameObject obstacleToDodge = GetClosestObstacle();

        if (obstacleToDodge)
        {
            if (Vector3.Distance(transform.position, obstacleToDodge.transform.position) < m_AttackRange + 1)
            {
                if (ObstacleBetweenEnemyAndPlayer())
                {
                    // we adapt the way we move
                    Debug.Log("adapt traj");

                }
            }
        }
    }

    private bool ObstacleBetweenEnemyAndPlayer()
    {
        Vector2 raycastDirection = m_DebugPlayer.transform.position - transform.position;
        raycastDirection.Normalize();
        //RaycastHit2D[] hited;

        Physics2D.Raycast(transform.position, raycastDirection, m_ContactFilter, m_Hited, 10);

        if (m_Hited.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
