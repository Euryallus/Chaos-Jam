using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField]
    private int  m_currentHealth = 1;
    [SerializeField]
    private int  m_maxHealth = 1;

    public AudioClip m_deathSound;

    // Start is called before the first frame update
    void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    public bool TakeDamage()
    {
        m_currentHealth--;
        if(m_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(m_deathSound, transform.position);
            return true;
        }
        else
        {
            return false;
        }
    }
}
