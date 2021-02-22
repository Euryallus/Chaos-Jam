using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{

    public int m_healthToRestore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.GetComponent<PlayerController>( ) != null)
        {
            collision.gameObject.GetComponent<HealthManager>( ).restoreHealth( m_healthToRestore );
            Destroy( gameObject );
        }
    }

}
