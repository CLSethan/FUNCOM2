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

    private float attackSpeed = 2;
    [SerializeField] private float attackTimer;

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        currentEnemyHealth = maxEnemyHealth;
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

    //private void OnTriggerEnter(Collider other) // Taking Damage
    //{
    //    currentEnemyHealth -= weaponDamage;

    //    if(currentEnemyHealth <= 0)
    //    {
    //        gameObject.SetActive(false);
    //        playerExperience += 1; // change to public variable in 'Upgrades' script
    //    }
    //}
}
