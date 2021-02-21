using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    #region PublicVariables

    [Header("Classes")]

    public PlayerController m_playerController;

    public BlackoutController m_blackOutScreen;

    [Space]

    [Header("Hit Points")]
    [Tooltip("The player's current health.")]
    public int m_currentHealth;

    [Tooltip("The sound that plays when the player dies.")]
    public AudioSource m_playerDeathSound;

    [SerializeField]
    [Tooltip("The slider that represents the player's health points.")]
    public Slider m_healthBar;

    #endregion

    #region PrivateVariables

    private const int m_maxHealth = 100;

    #endregion


    // Start is called before the first frame update
    void Start()
    {

        // Sets the value of currentHealth to be that of maxHealth
        m_currentHealth = m_maxHealth;

        //Sets the value of the healthbar slider to match that of the current health value
        m_healthBar.value = m_currentHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator waitToFade( )
    {

        yield return new WaitForSecondsRealtime(5);

        m_blackOutScreen.m_fadingIn = true;

    }

    public void takeDamage( int damageRecieved)
    {
        if (m_playerController.m_currentPlayerState != PlayerController.playerStates.dead)
        {
            m_currentHealth -= damageRecieved;

            m_healthBar.value = m_currentHealth;

            if (m_currentHealth <= 0)
            {
                m_playerController.enterDeathState();

                m_playerDeathSound.Play( );

                StartCoroutine(waitToFade());

            }
        }
    }

    public void resetHealth( )
    {

        m_currentHealth = m_maxHealth;

        m_healthBar.value = m_currentHealth;

    }

}
