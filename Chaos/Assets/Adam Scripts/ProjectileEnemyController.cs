using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyController : EnemyController
{

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
            GetComponentInChildren<EnemyProjectile>().Fire(m_targetPosition.normalized);
            m_attackTimer = 0;
        }
    }
}
