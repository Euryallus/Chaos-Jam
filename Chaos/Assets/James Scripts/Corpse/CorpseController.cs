using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseController : MonoBehaviour
{

    [HideInInspector]
    //The number of XP that the player had when they died
    public int m_savedXP;

    [HideInInspector]
    // The number of levels saved to the corpse when the player died
    public int m_savedLevels;

    public void createCorpse( int savedXP, int savedLevels, Transform playerTransform )
    {

        m_savedXP = savedXP;

        m_savedLevels = savedLevels;

        transform.position = playerTransform.position;

        gameObject.SetActive( true );

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if( collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerController>().m_currentPlayerState != PlayerController.playerStates.dead )
        {

            collision.gameObject.GetComponent<XPManager>().reclaimXP( m_savedXP );

            gameObject.SetActive( false );

        }

    }

}
