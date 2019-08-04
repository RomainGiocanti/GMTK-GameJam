using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] private GameObject m_ProjectilePrefab;
    [SerializeField] private float m_BulletSpeed = 0.034f;


    //----------------



    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        // if life </ 0 , trigger in aniamtion
    }

    // is tower so don't move
    protected override void TryMove()
    {
        //base.TryMove();
    }

    protected override void TryAttack()
    {
        base.TryAttack();
    }

    private void Shoot(Vector3 _DirectionToShoot)
    {
        Debug.Log("Shoot");

        Vector3 thisPos = transform.position;


        Vector3 posToSpawn = thisPos += (_DirectionToShoot * 0.01f);

        GameObject projectile = Instantiate(m_ProjectilePrefab, posToSpawn, transform.rotation);
        projectile.GetComponent<EnemyProjectile>().SetDirection(_DirectionToShoot, m_BulletSpeed);
    }

    protected override void Attack()
    {
        Vector3 directionToShoot = m_DebugPlayer.transform.position - transform.position;
        directionToShoot.Normalize();

        Shoot(directionToShoot);
    }

}
