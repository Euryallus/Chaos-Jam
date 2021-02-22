using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyController : EnemyController
{
    public EnemyProjectile m_projectile;

    public override void DetectPlayer()
    {
        if(Vector2.Distance(transform.position, m_targetPosition) > 8 && m_state == States.idle)
        {
            m_state = States.attack;
        }
    }

    public override void Attack()
    {
        if (Vector2.Distance(transform.position, m_targetPosition) > 8)
        {
            m_state = States.idle;
        }

        m_attackTimer += Time.deltaTime;

        if (m_attackTimer >= 3)
        {
            m_projectile.gameObject.SetActive(true);
            m_projectile.Fire(m_targetDirection);
            m_attackTimer = 0;
        }
    }
}
