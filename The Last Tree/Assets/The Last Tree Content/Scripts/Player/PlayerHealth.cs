using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public float currentHealth, maxHealth;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(float enemyDamage)
    {
        currentHealth -= enemyDamage;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("player has died");
        }
    }

    public void addHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Healed Player");
    }
}
