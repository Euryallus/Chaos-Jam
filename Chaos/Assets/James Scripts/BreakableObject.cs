using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    [Tooltip("The heart item that the vessel can drop.")]
    public GameObject m_heartItem;

    [Tooltip("The sound the item makes when it is broken.")]
    public AudioClip m_brokenSound;

    public void breakOpen( )
    {

        int randInt = Random.Range( 1, 100 );

        if( randInt <= 50)
        {
            // Instantiates a new health item at the object's position
            Instantiate(m_heartItem, transform.position, Quaternion.identity );
        }

        AudioSource.PlayClipAtPoint( m_brokenSound, transform.position );

        gameObject.SetActive( false );

    }

}
