﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    private float m_BasicMoveSpeed;
    private bool m_HasCharged = false;
    private bool m_EndCharge = false;
    private RaycastHit2D[] m_Hited = new RaycastHit2D[16];
    [SerializeField] private float m_MeleeAttackRange = 1f;

    public enum MeleeType
    {
        slime,
        skeleton,
        wooshie
    }

    [SerializeField] private MeleeType m_Type;

    private float m_LoadingTimeCharge = 1;
    private float m_TimerLoading = 0;
    private bool m_StartLoading = false;
    Vector3 m_PlaceToRush = Vector3.zero;
    private bool m_SetPlaceToRush = false;

    //----------------------------

    //obstacke avoidance

    [SerializeField] protected List<GameObject> m_Obstacles;



    private Vector2 m_CurrentPos;
    private Vector2 m_PrevPos;
    private Vector2 m_Velocity = Vector2.zero;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        m_BasicMoveSpeed = m_MoveSpeed;
        m_ContactFilter.SetLayerMask(LayerMask.NameToLayer("Player"));
        m_Animator = GetComponent<Animator>();
        m_CurrentPos = transform.position;
        m_PrevPos = m_CurrentPos;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        m_CurrentPos = transform.position;
        m_Velocity = m_CurrentPos - m_PrevPos;
        m_PrevPos = m_CurrentPos;

        if (m_Velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (life <= 0)
        {
            m_Animator.SetTrigger("Die");
        }


        if (m_StartLoading)
        {
            m_TimerLoading += Time.deltaTime;
            if (m_TimerLoading >= m_LoadingTimeCharge)
            {
                m_StartLoading = false;
                m_TimerLoading = 0;
                Charge();
                m_Animator.enabled = true;
                m_Animator.SetTrigger("Dash");
                Debug.Log("here");
            }
        }



        //soi trop loin ou arrivé au cac / la charge est finie
        if (m_Type == MeleeType.wooshie && m_PlaceToRush == Vector3.zero)
        {
            return;
        }

        if (CloseToGoal(m_PlaceToRush) || Vector2.Distance(m_DebugPlayer.transform.position, transform.position) > m_AttackRange)
        {
            if (m_HasCharged)
            {
                m_EndCharge = true;
            }

            if (Vector2.Distance(m_DebugPlayer.transform.position, transform.position) > m_AttackRange)
            {
                m_EndCharge = false;
                m_HasCharged = false;
                m_SetPlaceToRush = false;
                m_TimerLoading = 0;
            }
        }
    }

    protected override void TryMove()
    {
        if (life <= 0)
        {
            return;
        }
        base.TryMove();
    }

    protected override void TryAttack()
    {
        base.TryAttack();
    }

    protected override void Attack()
    {
        if (m_Type == MeleeType.slime)
        {
            SlimeAttack();
        }
        else if (m_Type == MeleeType.skeleton)
        {
            SkeletonAttack();
        }
        else if (m_Type == MeleeType.wooshie)
        {
            WooshieAttack();
        }

    }

    private void SlimeAttack()
    {
        if (InMeleeRange())
        {
            Debug.Log("MeleeAttack");
            //m_DebugPlayer.GetComponent<LivingBeing>().life -= m_AttackDamages;
        }
    }

    private void SkeletonAttack()
    {
        if (InMeleeRange())
        {
            Debug.Log("MeleeAttack");
            //m_DebugPlayer.GetComponent<LivingBeing>().life -= m_AttackDamages;
        }
    }

    private void WooshieAttack()
    {
        if (!m_HasCharged)
        {
            m_StartLoading = true;
            Debug.Log("disa");
            m_Animator.enabled = false;
            //Charge();
        }

        if (InMeleeRange())
        {
            Debug.Log("MeleeAttack");
            //m_DebugPlayer.GetComponent<LivingBeing>().life -= m_AttackDamages;
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
        if (m_Type != MeleeType.wooshie)
        {
            base.ChasePlayer();
        }
        else
        {
            if (m_StartLoading)
            {
                return;
            }

            m_DebugPlaceToGo = m_DebugPlayer.transform.position;
            if (!m_EndCharge)
            {
                Debug.Log("RUSHHHH");

                //Vector3 direction = m_DebugPlayer.transform.position - transform.position;
                if (!m_SetPlaceToRush)
                {
                    m_SetPlaceToRush = true;
                    m_PlaceToRush = m_DebugPlayer.transform.position;
                }
                Vector3 direction = m_PlaceToRush - transform.position;
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
            if (Vector2.Distance(obstacle.transform.position, transform.position) < distance)
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
                if (ObstacleBetweenEnemyAndPlayer(obstacleToDodge))
                {
                    // we adapt the way we move


                }
            }
        }
    }

    public void SetUpHitedAnimation()
    {
        if (life <= 0)
        {
            m_Animator.SetTrigger("Die");
        }
        else
        {
            m_Animator.SetTrigger("Hited");
        }
    }

    private bool ObstacleBetweenEnemyAndPlayer(GameObject _Go)
    {
        Vector2 raycastDirection = m_DebugPlayer.transform.position - transform.position;
        raycastDirection.Normalize();
        ////RaycastHit2D[] hited;

        //Physics2D.Raycast(transform.position, raycastDirection, m_ContactFilter, m_Hited, 10);

        //if (m_Hited.Length > 0)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}


        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, 10);

        if (hit.collider && hit.collider.CompareTag("Obstacle"))
        {
            return true;
        }

        return false;


        //if (m_Hited.Length > 0)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}
    }

}