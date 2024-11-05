using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Enemy Refs
    public Rigidbody2D theRigidbody;
    public float enemySpeed, enemyDamage, maxEnemyHealth;

    //created bool to check if the enemy attacks the player or tree
    public bool playerAttacker;
    [SerializeField] private float currentEnemyHealth;
    private Transform target;


    private float attackSpeed = 2;
    [SerializeField] private float attackTimer;

    //Player Experince
    [SerializeField] private PlayerExperience PlayerExp;
    [SerializeField] private int experienceAmount = 10;

    //Coin Refs
    public int coinValue;
    public float coinDropRate;

    //Health Refs
    public float healthDropRate;



    void Start()
    {
        // [Temporary] Randomizes whether the enemy attacks the player or the tree. 
        playerAttacker = UnityEngine.Random.value < 0.5f;

        currentEnemyHealth = maxEnemyHealth;
        PlayerExp = FindObjectOfType<PlayerExperience>();

        // if player attack find player transform
        if (playerAttacker)
        {
            target = FindObjectOfType<Player>().transform;
        }
        // if not player attacker find tree transform
        else if (!playerAttacker)
        {
            target = FindObjectOfType<TreeHealth>().transform;
        }

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
        // get components
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        TreeHealth treeHealth = collision.gameObject.GetComponent<TreeHealth>();
        
        if (treeHealth != null)
        {
            if (treeHealth.tag == "Tree" && attackTimer <= 0)
            {
                Debug.Log("Enemy is dealing damage to Tree");
                TreeHealth.instance.takeDamage(enemyDamage);
                attackTimer = attackSpeed;
            }
        }

        if (playerHealth != null)
        {
            if (playerHealth.tag == "Player" && attackTimer <= 0)
            {
                // Debug.Log("Enemy is dealing damage to Player");
                PlayerHealth.instance.takeDamage(enemyDamage);

                attackTimer = attackSpeed;
            }
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
                DropCoin();
                DropHealth();
                Destroy(this.gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy Took Damage");
        currentEnemyHealth -= damage;

        if (currentEnemyHealth < 0)
        {
            Debug.Log("Enemy has Died");
            Destroy(gameObject);
        }
    }

    void DropCoin()
    {
        if(UnityEngine.Random.value <= coinDropRate)
        {
            CoinController.instance.DropCoin(transform.position, coinValue);
        }
    }

    void DropHealth()
    {
        if (UnityEngine.Random.value <= healthDropRate)
        {
            HealthController.instance.DropHealth(transform.position);
        }
    }
}
