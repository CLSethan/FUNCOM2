using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    public static TreeHealth instance;

    public float currentHealth, maxHealth;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float enemyDamage)
    {
        currentHealth -= enemyDamage;

        if (currentHealth <= 0)
        {
            Debug.Log("tree has died");
        }
    }
}
