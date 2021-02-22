using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region PublicVariables

    [Header("Movement")]
    [Tooltip("The speed at which the player moves through the world.")]
    public float m_playerMoveSpeed;

    [Tooltip("The current velocity of the player, determines the direction that they are moving in.")]
    public Vector2 m_playerVelocity;

    [Tooltip("The RigidBody2D attached to this player.")]
    public Rigidbody2D m_rigidBody;

    public enum playerStates { alive, dead };

    [Space]

    [Header("Player States")]
    [Tooltip("The current state of the player. Determines what actions they are able to perform, as well as how their update function executes.")]
    public playerStates m_currentPlayerState;

    [Tooltip("The player's health manager script.")]
    public HealthManager m_healthManager;

    [Tooltip("A boolean that determines whether or not the player is currently attacking.")]
    public bool m_isAttacking;

    [Space]

    [Header("Experience Points")]
    [Tooltip("The player's XP Manager Script.")]
    public XPManager m_xpManager;

    [Tooltip("The Corpse Controller script attached to the player's corpse gameobject.")]
    public CorpseController m_corpseController;

    [Space]

    [Header("Sounds")]
    [Tooltip("The sound that plays when the player swings their sword.")]
    public AudioSource m_swordSwingSound;

    [Space]

    [Header("Animation")]
    [Tooltip("The animator for the player")]
    public Animator m_playerAnimator;

    [Space]

    [Header("User Interface")]
    [Tooltip("The player's HUD canvas.")]
    public GameObject m_hudCanvas;

    [Tooltip("The blackout screen shown when the player dies.")]
    public BlackoutController m_blackoutScreen;

    [Space]

    [Header("Weapons")]
    [Tooltip("The player's boomerang weapon")]
    public GameObject m_playerBoomerang;

    [Tooltip("The layer on which the enemies sit.")]
    public LayerMask m_enemyLayer;

    [Tooltip("The layer that the player sits on, used in raycasts to ignore the player.")]
    public LayerMask m_playerLayer;

    #endregion

    #region PrivateVariables

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

                    if (!m_isAttacking)
                    {
                        // Calls the function to check for any keyboard inputs
                        checkForInput();
                    }

                    break;
                }
            case ( playerStates.dead ):
                {

                    break;
                }
        }

    }

    public void enterDeathState( )
    {

        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        m_hudCanvas.SetActive( false );

        m_corpseController.createCorpse( m_xpManager.m_totalXP, m_xpManager.m_currentLevel, transform );

        m_currentPlayerState = playerStates.dead;

    }

    public void meleeAttack( )
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, m_playerVelocity, 1, m_enemyLayer );

        if( rayHit.collider != null)
        {
            if( rayHit.collider.gameObject.GetComponent<EnemyHealthManager>( ) != null )
            {
                rayHit.collider.gameObject.GetComponent<EnemyHealthManager>( ).TakeDamage( );
                m_xpManager.gainXP( );
            }
            else if( rayHit.collider.gameObject.GetComponent<BreakableObject>( ) != null )
            {
                rayHit.collider.gameObject.GetComponent<BreakableObject>( ).breakOpen( );
            }
        }

    }

    public void respawnPlayer( )
    {

        // Resets the player character's position to the initial room
        transform.position = transform.parent.position;

        // Resets them player's XP values
        m_xpManager.resetXP( );

        m_healthManager.resetHealth( );

        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        StartCoroutine( waitToFade( ) );

        m_currentPlayerState = playerStates.alive;

    }

    public void throwBoomerang( )
    {
        // Sets the position of the boomerang to that of the player
        m_playerBoomerang.transform.position = transform.position;

        // Enables the boomerang and calls its targeting function
        m_playerBoomerang.SetActive(true);

    }

    private void checkForInput()
    {

        #region MovementControls

        // If the A key is pressed, the player turns to the left
        if ( Input.GetKeyDown( KeyCode.A ) )
        {

            // Sets the weight of the current animation layer to 0 to "disable" it
            m_playerAnimator.SetLayerWeight(m_currentVelocityIndex + 1, 0);

            // The value of the velocity array index is incremented
            m_currentVelocityIndex++;

            // If the new value is outside the bounds of the array, it is reset to 0
            if( m_currentVelocityIndex > 3)
            {
                m_currentVelocityIndex = 0;
            }

            // Sets the weight of the animation layer at the new index address to 1 to "enable" it
            m_playerAnimator.SetLayerWeight(m_currentVelocityIndex + 1, 1);

            // Sets the player's current velocity to be that of the vector2 at the current index in the array
            m_playerVelocity = m_directionalVelocities[m_currentVelocityIndex];
        }

        // If the X key is pressed, the player turns to the right
        else if( Input.GetKeyDown( KeyCode.X ))
        {

            // Sets the weight of the current animation layer to 0 to "disable" it
            m_playerAnimator.SetLayerWeight( m_currentVelocityIndex + 1, 0 );

            // The value of the velocity array index is decremented
            m_currentVelocityIndex--;

            // If the new value is outside the bounds of the array, it is reset to 3
            if ( m_currentVelocityIndex < 0 )
            {
                m_currentVelocityIndex = 3;
            }

            // Sets the weight of the animation layer at the new index address to 1 to "enable" it
            m_playerAnimator.SetLayerWeight( m_currentVelocityIndex + 1, 1 );

            // Sets the player's current velocity to be that of the vector2 at the current index in the array
            m_playerVelocity = m_directionalVelocities[m_currentVelocityIndex];
        }

        if( Input.GetKey( KeyCode.Space ) )
        {
            // Moves the player by their speed, in the direction defined by their velocity. Multiplied by deltaTime to make it framerate independent
            m_rigidBody.velocity = m_playerVelocity * m_playerMoveSpeed * Time.fixedDeltaTime;

            // Triggers the player's animation to transition to walking
            m_playerAnimator.SetBool( "isMoving", true );

        }
        else if( Input.GetKeyUp( KeyCode.Space ))
        {

            // Resets the player's velocity to 0 so they don't continue to move
            m_rigidBody.velocity = new Vector2( 0, 0);

            // Triggers the player's animation to transition back to idle
            m_playerAnimator.SetBool("isMoving", false);

            // Triggers the player's animation to transition back to idle
            m_playerAnimator.SetBool( "isMoving", false );

        }

        #endregion

        #region CombatControls

        if (!m_isAttacking)
        {

            if ( Input.GetKeyDown( KeyCode.B ) )
            {

                // Sets the player's velocity to 0 so that the player cannot move while attacking
                m_rigidBody.velocity = new Vector2( 0, 0 );

                // Triggers the melee attack animation to play on the player
                m_playerAnimator.SetTrigger("throwBoomerang");

            }

            else if ( Input.GetKeyDown( KeyCode.P ) )
            {

                // Sets the player's velocity to 0 so that the player cannot move while attacking
                m_rigidBody.velocity = new Vector2(0, 0);

                // Triggers the melee attack animation to play on the player
                m_playerAnimator.SetTrigger("meleeAttack");

                // Plays the sound of the player's sword being swung
                m_swordSwingSound.Play( );

            }

        }

        #endregion

    }

    private IEnumerator waitToFade()
    {

        yield return new WaitForSecondsRealtime(5);

        m_hudCanvas.SetActive(true);

        m_blackoutScreen.m_fadingOut = true;

    }

}
