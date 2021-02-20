﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField]
    private int  m_currentHealth = 1;
    [SerializeField]
    private int  m_maxHealth = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    public void TakeDamage()
    {
        m_currentHealth--;
        if(m_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
