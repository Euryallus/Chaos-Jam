using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region PublicVariables

    [Header("Movement")]
    [Space]
    [Tooltip("The speed at which the player moves through the world.")]
    public float m_playerMoveSpeed;

    public enum playerStates { alive, dead };

    [Header("Player States")]
    [Tooltip("The current state of the player. Determines what actions they are able to perform, as well as how their update function executes.")]
    public playerStates m_currentPlayerState;

    #endregion

    #region PrivateVariables

    // The current velocity of the player, determines the direction that they are moving in
    private Vector2 m_playerVelocity;

    private int m_currentVelocityIndex;

    private Vector2[] m_directionalVelocities = new Vector2[4] { new Vector2( 0, 1 ), new Vector2( -1, 0 ), new Vector2( 0, -1 ), new Vector2( 1, 0 ) };

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Sets the initial state of the player to Alive
        m_currentPlayerState = playerStates.alive;

        // Sets the player's velocity to 0, 1 so that they move up the screen from the start
        m_playerVelocity = m_directionalVelocities[m_currentVelocityIndex];

    }

    // Update is called once per frame
    void Update()
    {
        
        switch( m_currentPlayerState)
        {
            case ( playerStates.alive ):
                {
                    // Calls the function to check for any keyboard inputs
                    checkForInput( );

                    break;
                }
            case (playerStates.dead):
                {

                    break;
                }
        }

    }

    private void checkForInput()
    {

        // If the A key is pressed, the player turns to the left
        if( Input.GetKeyDown( KeyCode.A ) )
        {
            // The value of the velocity array index is incremented
            m_currentVelocityIndex++;

            // If the new value is outside the bounds of the array, it is reset to 0
            if( m_currentVelocityIndex > 3)
            {
                m_currentVelocityIndex = 0;
            }
            // Sets the player's current velocity to be that of the vector2 at the current index in the array
            m_playerVelocity = m_directionalVelocities[m_currentVelocityIndex];
        }

        // If the X key is pressed, the player turns to the right
        else if( Input.GetKeyDown( KeyCode.X ))
        {
            // The value of the velocity array index is decremented
            m_currentVelocityIndex--;

            // If the new value is outside the bounds of the array, it is reset to 3
            if ( m_currentVelocityIndex < 0 )
            {
                m_currentVelocityIndex = 3;
            }
            // Sets the player's current velocity to be that of the vector2 at the current index in the array
            m_playerVelocity = m_directionalVelocities[m_currentVelocityIndex];
        }

        if( Input.GetKey( KeyCode.Space ) )
        {
            // Moves the player by their speed, in the direction defined by their velocity. Multiplied by deltaTime to make it framerate independent
            transform.Translate(m_playerVelocity * m_playerMoveSpeed * Time.deltaTime);
        }

    }

}
