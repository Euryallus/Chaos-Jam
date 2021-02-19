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

    [Tooltip("Determines whether or not the boomerang has found any targets, dicating how it moves.")]
    public bool m_hasFoundTargets;

    [Space]

    #endregion

    #region PrivateVariables
    private Collider2D[] m_currentTargetsColliders;

    #endregion

    private void OnEnable()
    {
        m_currentTargetsColliders = Physics2D.OverlapCircleAll(transform.position, m_throwRange);

        if( m_currentTargetsColliders.Length == 0)
        {
            Debug.Log("No targets found!");
            m_hasFoundTargets = false;
        }
        else
        {
            sortTargetsByClosest( );
            m_hasFoundTargets = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( !m_hasFoundTargets )
        {
            transform.Translate( new Vector2( 0, 1 ) * m_moveSpeed * Time.deltaTime );
        }

        transform.Rotate( transform.up * m_moveSpeed );

    }

    private void sortTargetsByClosest( )
    {
        int i, j;
        int N = m_currentTargetsColliders.Length;

        for( j = N - 1; j > 0; j--)
        {
            for( i = 0; i < j; i++)
            {
                // Calculates the distance between the current collider and the boomerang
                Vector2 colliderOnePos = new Vector2( m_currentTargetsColliders[i].gameObject.transform.position.x, m_currentTargetsColliders[i].gameObject.transform.position.y );
                Vector2 colliderOneDistanceFromRang = new Vector2( colliderOnePos.x - transform.position.x, colliderOnePos.y - transform.position.y );

                // Calculates the distance between the current collider and the boomerang
                Vector2 colliderTwoPos = new Vector2(m_currentTargetsColliders[i + 1].gameObject.transform.position.x, m_currentTargetsColliders[i + 1].gameObject.transform.position.y);
                Vector2 colliderTwoDistanceFromRang = new Vector2(colliderTwoPos.x - transform.position.x, colliderTwoPos.y - transform.position.y);

                //If the distance between the current boomerang is greater than the next in the array, the two are swapped around
                if ( colliderOneDistanceFromRang.x > colliderTwoDistanceFromRang.x && colliderOneDistanceFromRang.y > colliderTwoDistanceFromRang.y )
                {
                    swapValues(m_currentTargetsColliders, i, i + 1 );
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
