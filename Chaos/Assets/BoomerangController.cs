using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{

    #region PublicVariables

    [Header("Movement")]
    [Tooltip("The range that the boomerang will detect targets within when first thrown.")]
    public float m_throwRange;

    [Tooltip("The speed at which the boomerang moves when in flight.")]
    public float m_moveSpeed;

    [Tooltip("The direction in which the boomerang will move. Inherited from the player.")]
    public Vector2 m_moveDirection;

    [Tooltip("The velocity used when the boomerang returns to the player.")]
    public Vector2 m_returnVelocity = Vector2.zero;

    [Tooltip("The player object. Used to inherit information about the player.")]
    public GameObject m_playerObject;

    [Tooltip("Determines whether or not the boomerang has found any targets, dicating how it moves.")]
    public bool m_hasFoundTargets;

    [Tooltip("Determines whether or not the boomerang is currently returning to the player")]
    public bool m_isReturning;

    [Space]

    [Header("Physics")]
    [Tooltip("The physics layer of the enemies.")]
    public LayerMask m_enemyLayer;


    #endregion

    #region PrivateVariables

    private int m_currentTargetIndex;

    private Collider2D[] m_currentTargetsColliders;

    #endregion

    private void OnEnable()
    {

        m_moveDirection = m_playerObject.GetComponent<PlayerController>().m_playerVelocity;

        m_currentTargetsColliders = Physics2D.OverlapCircleAll( transform.position, m_throwRange, m_enemyLayer );

        if( m_currentTargetsColliders.Length == 0)
        {
            Debug.Log("No targets found!");
            m_hasFoundTargets = false;
        }
        else
        {
            Debug.Log("Found targets!");
            sortTargetsByClosest( );
            m_hasFoundTargets = true;
        }
    }

    private void OnDisable()
    {
        m_isReturning = false;

        m_hasFoundTargets = false;

        m_currentTargetIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if ( !m_hasFoundTargets && !m_isReturning )
        {
            if( Vector2.Distance( transform.position, m_playerObject.transform.position ) > m_throwRange )
            {
                m_isReturning = true;
            }
            else
            {
                transform.Translate(m_moveDirection * m_moveSpeed * Time.deltaTime);
            }
        }
        else if( m_hasFoundTargets && !m_isReturning )
        {
            if( attackEnemies( ) )
            {
                m_isReturning = true;
            }
        }
        else if (m_isReturning)
        {
            returnToPlayer( );
        }
    }

    private bool attackEnemies( )
    {

        transform.position = Vector2.MoveTowards( transform.position, m_currentTargetsColliders[m_currentTargetIndex].gameObject.transform.position, m_moveSpeed * Time.deltaTime );

        if( Vector2.Distance( transform.position, m_currentTargetsColliders[m_currentTargetIndex].gameObject.transform.position ) <= 0 )
        {
            // Hurt the enemy here pls

            m_currentTargetIndex++;

            if( m_currentTargetIndex >= m_currentTargetsColliders.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool moveToTarget( Transform targetTransform)
    {

        if( Vector2.Distance( transform.position, targetTransform.position ) > Mathf.Epsilon )
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, m_moveSpeed * Time.deltaTime );
            return false;
        }
        else
        {
            transform.position = targetTransform.position;
            return true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Debug.Log("Triggered! It was: " + collision.gameObject.name);
            m_isReturning = true;
        }
    }

    private void returnToPlayer( )
    {
        if( moveToTarget( m_playerObject.transform ) )
        {
            gameObject.SetActive( false );
        }
    }

    private void sortTargetsByClosest( )
    {
        int i, j;
        int N = m_currentTargetsColliders.Length;
        float numSwaps = 0;
        bool hasSwapped = true;

        while (hasSwapped)
        {

            for (j = N - 1; j > 0; j--)
            {
                for (i = 0; i < j; i++)
                {
                    // Calculates the distance between the current collider and the boomerang
                    float colliderOneDistanceFromRang = Vector2.Distance(m_currentTargetsColliders[i].gameObject.transform.position, transform.position);

                    // Calculates the distance between the current collider and the boomerang
                    float colliderTwoDistanceFromRang = Vector2.Distance(m_currentTargetsColliders[i].gameObject.transform.position, transform.position);

                    // If the distance between the current boomerang is greater than the next in the array, the two are swapped around
                    if (colliderOneDistanceFromRang > colliderTwoDistanceFromRang)
                    {
                        swapValues(m_currentTargetsColliders, i, i + 1);
                    }
                }
                // If no swaps have been performed, then the array is sorted and the loop can be broken
                if (numSwaps == 0)
                {
                    hasSwapped = false;
                }
            }

        }

    }

    private static void swapValues( Collider2D[] colliders, int colliderOneIndex, int colliderTwoIndex)
    {

        Collider2D tempCollider;

        tempCollider = colliders[colliderOneIndex];
        colliders[colliderOneIndex] = colliders[colliderTwoIndex];
        colliders[colliderTwoIndex] = tempCollider;

    }

}
