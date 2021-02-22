using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutController : MonoBehaviour
{

    public SpriteRenderer m_blackoutScreen;

    public PlayerController m_playerController;

    [HideInInspector]
    public bool m_fadingIn;

    [HideInInspector]
    public bool m_fadingOut;

    // Start is called before the first frame update
    void Start()
    {
        m_blackoutScreen.color = new Color( 1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if( m_fadingIn )
        {

            m_blackoutScreen.color = Color.Lerp( m_blackoutScreen.color, Color.black, 0.01f );
            if ( m_blackoutScreen.color.a >= 0.9 )
            {

                m_blackoutScreen.color = new Color( 0, 0, 0, 1);

                m_fadingIn = false;

                m_playerController.respawnPlayer();

            }
        }
        else if( m_fadingOut )
        {

            m_blackoutScreen.color = Color.Lerp( m_blackoutScreen.color, Color.clear, 0.01f );

            if( m_blackoutScreen.color.a <= 0.1 )
            {

                m_blackoutScreen.color = new Color( 0, 0, 0, 0 );

                m_fadingOut = false;

            }

        }

    }

}
