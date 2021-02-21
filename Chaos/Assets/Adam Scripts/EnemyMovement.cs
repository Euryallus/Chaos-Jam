using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
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
    
    [SerializeField]
    private  Vector3    m_targetDirection;
    
    private Animator    m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_playerObject = GameObject.FindGameObjectWithTag("Player");

        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_targetPosition = m_playerObject.transform.position;

        m_targetDirection = m_targetPosition - new Vector2(transform.position.x, transform.position.y);
       
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

            m_step = m_speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, m_targetPosition, m_step);
        }

        if(m_state == States.attack)
        {
            if (Vector2.Distance(transform.position, m_targetPosition) > 1)
            {
                m_state = States.chase;
            }

            m_attackTimer += Time.deltaTime;

            if(m_attackTimer >= 1)
            {
                m_playerObject.GetComponent<HealthManager>().takeDamage(m_dps);
                m_attackTimer = 0;
            }
        }
    }
}