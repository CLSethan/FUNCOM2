using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //GREX: Added animator variables for animations & collider variable
    private Animator anim;
    [SerializeField] private RuntimeAnimatorController idleAnimatorController;
    [SerializeField] private RuntimeAnimatorController runAnimatorController;
    [SerializeField] private RuntimeAnimatorController attackAnimatorController;
    [SerializeField] private RuntimeAnimatorController deathAnimatorController;
    [SerializeField] private Collider2D enemyCollider;

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

    private bool canMove = true;
    //GREX: Added bools for death and attack anims
    public bool isDead { get; private set; } = false;
    private bool isAttacking = false;

    //GREX: Added Awake to get animator when enemy spawns
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
    }

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
        FlipCharacterSprite();

        if (canMove)
        {
            theRigidbody.velocity = (target.position - transform.position).normalized * enemySpeed;

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
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
                //GREX: Added a bool check for when its attacking to activate attack animation
                isAttacking = true;
                anim.runtimeAnimatorController = attackAnimatorController;

                Debug.Log("Enemy is dealing damage to Tree");
                TreeHealth.instance.takeDamage(enemyDamage);
                attackTimer = attackSpeed;

                StartCoroutine(EnemyAttackTime());
            }
        }

        if (playerHealth != null)
        {
            if (playerHealth.tag == "Player" && attackTimer <= 0)
            {
                isAttacking = true;
                anim.runtimeAnimatorController = attackAnimatorController;
                theRigidbody.isKinematic = true;

                // Debug.Log("Enemy is dealing damage to Player");
                PlayerHealth.instance.takeDamage(enemyDamage);

                attackTimer = attackSpeed;

                StartCoroutine(EnemyAttackTime());
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

            //GREX: Commented this out since there's already a check for when currentEnemyHealth is below or equal to 0 in the TakeDamage function
            /*            if (currentEnemyHealth <= 0)
                        {
                            PlayerExp.AddExperience(experienceAmount); // public variable in 'Upgrades' script
                            DropCoin();
                            DropHealth();
                            anim.runtimeAnimatorController = deathAnimatorController;
                            StartCoroutine(DestroyEnemy());
                        }*/
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        Debug.Log("Enemy Took Damage");
        currentEnemyHealth -= damage;
        DamageNumberController.instance.SpawnDamage(damage, transform.position);

        //GREX: Moved the death logic in TakeDamage function
        if (currentEnemyHealth <= 0 && !isDead)
        {
            EnemyDeath();
        }
    }

    //GREX: Added EnemyDeath function to handle death logic
    void EnemyDeath()
    {
        isDead = true;
        canMove = false;

        //GREX: Added rigidbody and collider disables when enemy dies

        enemyCollider.enabled = false;
        theRigidbody.simulated = false;

        PlayerExp.AddExperience(experienceAmount); // public variable in 'Upgrades' script
        DropCoin();
        DropHealth();
        anim.runtimeAnimatorController = deathAnimatorController;
        StartCoroutine(DestroyEnemy());
    }

    void DropCoin()
    {
        if (UnityEngine.Random.value <= coinDropRate)
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

    //GREX: Added timer for Destroying enemyGameObject for enemy to perform death animation before getting destroyed
    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }

    private IEnumerator EnemyAttackTime()
    {
        yield return new WaitForSeconds(0.4f);

        isAttacking = false;
    }

    public IEnumerator movementStunLock(float stunDuration, bool willStun)
    {
        canMove = false;
        yield return new WaitForSeconds(stunDuration);
        canMove = true;
        yield return null;
    }

    //GREX: Added FlipCharacterSprite for enemy's sprite to flip based on their velocity
    private void FlipCharacterSprite()
    {
        if (isDead)
            return;

        if (!isAttacking)
        {
            if (theRigidbody.velocity.x > 0)
            {
                anim.runtimeAnimatorController = runAnimatorController;
                transform.localScale = new Vector3(4f, 4f, 4f);
            }

            if (theRigidbody.velocity.x < 0)
            {
                anim.runtimeAnimatorController = runAnimatorController;
                transform.localScale = new Vector3(-4f, 4f, 4f);
            }

            if (theRigidbody.velocity.x == 0)
            {
                anim.runtimeAnimatorController = idleAnimatorController;
            }
        }
    }
}
