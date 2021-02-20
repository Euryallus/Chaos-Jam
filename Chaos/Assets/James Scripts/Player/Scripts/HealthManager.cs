using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    #region PublicVariables

    [Header("Classes")]

    public PlayerController m_playerController;

    [Space]

    [Header("Hit Points")]
    [Tooltip("The player's current health.")]
    public int m_currentHealth;

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

    public void takeDamage( int damageRecieved)
    {

        m_currentHealth -= damageRecieved;

        m_healthBar.value = m_currentHealth;

        if( m_currentHealth <= 0)
        {
            m_playerController.m_currentPlayerState = PlayerController.playerStates.dead;
        }
    }

}
