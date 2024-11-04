using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public float currentHealth, maxHealth;

    public Slider healthSlider;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        UIController.Instance.UpdateHealth();

    }

    public void takeDamage(float enemyDamage)
    {
        currentHealth -= enemyDamage;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("player has died");
        }

        healthSlider.value = currentHealth;
        UIController.Instance.UpdateHealth();

    }

    public void addHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Healed Player");
        healthSlider.value = currentHealth;

        UIController.Instance.UpdateHealth();

    }
}
