using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemyMovementPlayer : MonoBehaviour
{
    public Rigidbody2D theRigidbody;
    public float enemySpeed, enemyDamage, maxEnemyHealth;
    [SerializeField] private float currentEnemyHealth;
    private Transform target;

    [SerializeField] private PlayerExperience PlayerExp;

    private float attackSpeed = 2;
    [SerializeField] private float attackTimer;

    [SerializeField] private int experienceAmount = 10;

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        currentEnemyHealth = maxEnemyHealth;

        PlayerExp = FindObjectOfType<PlayerExperience>();
    }

    void FixedUpdate()
    {
        theRigidbody.velocity = (target.position - transform.position).normalized * enemySpeed;

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerHealth.instance.tag == "Player" && attackTimer <= 0)
        {
            Debug.Log("enemy is dealing damage to Player");
            PlayerHealth.instance.takeDamage(enemyDamage);

            attackTimer = attackSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            TakeDamage(projectile.GetDamage());
            projectile.OnHit(); // optional if the projectile has specific behavior on hit (e.g., destroy itself)

            if (currentEnemyHealth <= 0)
            {
                PlayerExp.AddExperience(experienceAmount); // public variable in 'Upgrades' script
                Destroy(this.gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentEnemyHealth -= damage;
    }
}
