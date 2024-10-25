using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemyMovementTree : MonoBehaviour
{
    //defunct code to be deleted
    
    //public Rigidbody2D theRigidbody;
    //public float enemySpeed, enemyDamage, maxEnemyHealth;
    //[SerializeField] private float currentEnemyHealth;
    //private Transform target;

    //[SerializeField] public float attackSpeed;
    //[SerializeField] public float attackTimer;

    //void Start()
    //{
    //    target = FindObjectOfType<TreeHealth>().transform;
    //    currentEnemyHealth = maxEnemyHealth;
    //}

    //void Update()
    //{
    //    theRigidbody.velocity = (target.position - transform.position).normalized * enemySpeed;

    //    if (attackTimer > 0)
    //    {
    //        attackTimer -= Time.deltaTime;
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (TreeHealth.instance.tag == "Tree" && attackTimer <= 0)
    //    {
    //        Debug.Log("enemy is dealing damage to Tree");
    //        TreeHealth.instance.takeDamage(enemyDamage);

    //        attackTimer = attackSpeed;
    //    }
    //}

    //private void OnTriggerEnter(Collider other) // Taking Damage
    //{
    //    currentEnemyHealth -= weaponDamage;

    //    if (currentEnemyHealth <= 0)
    //    {
    //        gameObject.SetActive(false);
    //        playerExperience += 1; // change to public variable in 'Upgrades' script
    //    }
    //}
}
