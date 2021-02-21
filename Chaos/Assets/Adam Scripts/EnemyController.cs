using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum States
    {
        chase,
        attack,
        idle,
        patrol
    }

    [SerializeField]
    private States      m_state = States.idle;

    [SerializeField]
    private float       m_speed = 1.5f;

    [SerializeField]
    private int         m_dps = 1;

    private GameObject  m_playerObject;
    private float       m_step;
    private float       m_attackTimer;
    private Vector2     m_targetPosition;
    private Vector2     m_velocity;
    
    [SerializeField]
    private Vector2     m_targetDirection;
    
    private Animator    m_animator;
    private Rigidbody2D m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_playerObject = GameObject.FindGameObjectWithTag("Player");

        m_animator = GetComponent<Animator>();

        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_targetPosition = m_playerObject.transform.position;

        m_targetDirection = m_targetPosition - new Vector2(transform.position.x, transform.position.y);

        Animate();

        if (Vector2.Distance(transform.position, m_targetPosition) < 8 && m_state == States.idle)
        {
            m_state = States.chase;
        }

        if (m_state == States.chase)
        {
            if (Vector2.Distance(transform.position, m_targetPosition) < 0.8)
            {
                m_state = States.attack;
            }

            m_rigidbody.velocity = m_targetDirection * m_speed * Time.deltaTime;
        }

        if(m_state == States.attack)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        if (Vector2.Distance(transform.position, m_targetPosition) > 1)
        {
            m_state = States.chase;
        }

        m_attackTimer += Time.deltaTime;
        m_rigidbody.velocity = new Vector2(0, 0);

        if (m_attackTimer >= 1)
        {
            m_playerObject.GetComponent<HealthManager>().takeDamage(m_dps);
            m_attackTimer = 0;
        }
    }

    public virtual void Animate()
    {
        if (m_targetDirection.x > 0 && m_targetDirection.y <= 1.5 && m_targetDirection.y >= -1.5)
        {
            m_animator.SetLayerWeight(4, 1);
            m_animator.SetLayerWeight(1, 0);
            m_animator.SetLayerWeight(2, 0);
            m_animator.SetLayerWeight(3, 0);
        }
        if (m_targetDirection.x < 0 && m_targetDirection.y <= 1.5 && m_targetDirection.y >= -1.5)
        {
            m_animator.SetLayerWeight(3, 1);
            m_animator.SetLayerWeight(1, 0);
            m_animator.SetLayerWeight(2, 0);
            m_animator.SetLayerWeight(4, 0);
        }

        if (m_targetDirection.y > 0 && m_targetDirection.x <= 1.5 && m_targetDirection.x >= -1.5)
        {
            m_animator.SetLayerWeight(1, 1);
            m_animator.SetLayerWeight(2, 0);
            m_animator.SetLayerWeight(3, 0);
            m_animator.SetLayerWeight(4, 0);
        }
        if (m_targetDirection.y < 0 && m_targetDirection.x <= 1.5 && m_targetDirection.x >= -1.5)
        {
            m_animator.SetLayerWeight(2, 1);
            m_animator.SetLayerWeight(1, 0);
            m_animator.SetLayerWeight(3, 0);
            m_animator.SetLayerWeight(4, 0);
        }
    }
}