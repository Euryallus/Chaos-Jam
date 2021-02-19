using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    #region PublicVariables

    [Header("Classes")]

    public PlayerController m_playerController;

    [Space]

    [Header("Hit Points")]

    public int m_currentHealth;

    #endregion

    #region PrivateVariables

    private const int m_maxHealth = 100;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        // Sets the value of currentHealth to be that of maxHealth
        m_currentHealth = m_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
