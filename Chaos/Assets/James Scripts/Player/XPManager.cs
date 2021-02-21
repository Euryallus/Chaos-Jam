using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class XPManager : MonoBehaviour
{

    #region Experience Points

    [Header("Experience Points")]

    [Tooltip("The number of XP that the player has currently gained.")]
    public int m_currentXP;

    [Tooltip("The player's current XP level.")]
    public int m_currentLevel;

    [Header("User Interface")]
    [Tooltip("The slider used to represent the player's current XP.")]
    public Slider m_XPSlider;

    [Tooltip("The text that displays the player's current XP.")]
    public Text m_currentXPText;

    [Tooltip("The text that displays the player's current level.")]
    public Text m_currentLevelText;

    // The amount of XP required for the player to level up.
    private const int m_xpToNextLevel = 500;

    #endregion

    private void Awake()
    {
        // Sets the player's current level to 1
        m_currentLevel = 1;

        m_XPSlider.value = m_currentXP;

        m_currentXPText.text = m_currentXP.ToString( ) + " / " + m_xpToNextLevel.ToString( );

        m_currentLevelText.text = m_currentLevel.ToString( );

    }

    public void gainXP( int xpGained = 15 )
    {
        // Local variable to store the target XP level of the while loop
        int newCurrentXP = m_currentXP + xpGained;

        while( m_currentXP < newCurrentXP)
        {
            // Increments the current XP by one to increase it over time
            m_currentXP++;

            // Updates the value of the XP slider bar
            m_XPSlider.value = m_currentXP;

            // Updates the currentXP text to use the new value
            m_currentXPText.text = m_currentXP.ToString( ) + " / " + m_xpToNextLevel.ToString( );

        }

        if( m_currentXP > m_xpToNextLevel)
        {
            // Raises the player to the next XP level
            m_currentLevel++;

            // Resets the player's current XP points to 0 + the amount they went over the next level threshold
            m_currentXP -= m_xpToNextLevel;
        }

    }

}
