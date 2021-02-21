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

    [SerializeField]
    [Tooltip("The delay between each XP point increase when XP is gained")]
    private float m_xpGainDelay;

    [Header("Sounds")]
    [Tooltip("The sound played when the player gains XP.")]
    public AudioSource m_xpGainSound;

    [Tooltip("The sound played when the player levels up.")]
    public AudioSource m_levelUpSound;

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

        m_currentLevelText.text = "Level: " + m_currentLevel.ToString( );

    }


    public IEnumerator AddExperience( int experienceToAdd )
    {

        // Gradually increases the player's current XP over time
        for (int i = 0; i < experienceToAdd; i++)
        {
            // Increments the current XP by one to increase it over time
            m_currentXP++;

            if (m_currentXP > m_xpToNextLevel)
            {
                // Raises the player to the next XP level
                m_currentLevel++;

                // Resets the player's current XP points to 0 + the amount they went over the next level threshold
                m_currentXP -= m_xpToNextLevel;

                // Updates the player's XP Bar to use the new value
                m_XPSlider.value = m_currentXP;

                // Updates the content of the XP Text UI to use the new XP value
                m_currentXPText.text = m_currentXP.ToString() + " / " + m_xpToNextLevel.ToString();

                // Sets the text of the Level UI element to reflect the player's new XP level
                m_currentLevelText.text = "Level: " + m_currentLevel.ToString();

                // Plays the Level Up sound to alert the layer that they have levelled up
                m_levelUpSound.Play( );

            }

            // Updates the value of the XP slider bar
            m_XPSlider.value = m_currentXP;

            // Updates the currentXP text to use the new value
            m_currentXPText.text = m_currentXP.ToString() + " / " + m_xpToNextLevel.ToString();

            yield return new WaitForSeconds(m_xpGainDelay);
        }

    }

    public void gainXP( int xpGained = 260 )
    {

        StartCoroutine( AddExperience( xpGained ) );

        if( !m_xpGainSound.isPlaying)
        {
            m_xpGainSound.Play( );
        }

    }

}
