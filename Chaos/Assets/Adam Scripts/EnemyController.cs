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
    protected States    m_state = States.idle;

    [SerializeField]
    private float       m_speed = 100;

    [SerializeField]
    private int         m_dps = 1;

    private GameObject  m_playerObject;
    protected float     m_attackTimer;
    protected Vector2   m_targetPosition;

    [SerializeField]
    protected Vector2     m_targetDirection;
    
    private Animator    m_animator;
    private Rigidbody2D m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_playerObject = GameObject.FindGameObjectWithTag("Player");

        m_animator = GetComponent<Animator>();

        m_rigidbody = GetComponent<Rigidbody2D>();

        m_animator.SetLayerWeight(2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        m_targetPosition = m_playerObject.transform.position;

        m_targetDirection = m_targetPosition - new Vector2(transform.position.x, transform.position.y);

        Animate();

        DetectPlayer();

        if (m_state == States.chase)
        {
            if (Vector2.Distance(transform.position, m_targetPosition) < 1)
            {
                m_state = States.attack;
            }

            m_animator.SetBool("Active", true);

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
        // Up
        if (m_targetDirection.y > 0 && m_targetDirection.x <= 1.5 && m_targetDirection.x >= -1.5)
        {
            ResetAnimatorLayers();

            m_animator.SetLayerWeight(1, 1);
        }

        //Down
        if (m_targetDirection.y < 0 && m_targetDirection.x <= 1.5 && m_targetDirection.x >= -1.5)
        {
            ResetAnimatorLayers();

            m_animator.SetLayerWeight(2, 1);
        }

        //Left
        if (m_targetDirection.x < 0 && m_targetDirection.y <= 1.5 && m_targetDirection.y >= -1.5)
        {
            ResetAnimatorLayers();

            m_animator.SetLayerWeight(3, 1);
        }

        //Right
        if (m_targetDirection.x > 0 && m_targetDirection.y <= 1.5 && m_targetDirection.y >= -1.5)
        {
            ResetAnimatorLayers();

            m_animator.SetLayerWeight(4, 1);
        }
    }

    public virtual void DetectPlayer()
    {
        if (Vector2.Distance(transform.position, m_targetPosition) < 8 && m_state == States.idle && m_playerObject.GetComponent<PlayerController>().m_currentPlayerState == PlayerController.playerStates.alive)
        {
            m_state = States.chase;
        }
        else
        {
            m_state = States.idle;
        }
    }

    private void ResetAnimatorLayers()
    {
        for (int i = 0; i < 5; i++)
        {
            m_animator.SetLayerWeight(i, 0);
        }
    }
}