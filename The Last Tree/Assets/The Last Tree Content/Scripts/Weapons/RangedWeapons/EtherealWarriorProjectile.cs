using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EtherealWarriorProjectile : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private float rightMovementOffset = 4f;
    [SerializeField] private EtherealWarrior EtherealWarriorScript;
    [SerializeField] private GameObject EtherealWarriorGameObject;
    [SerializeField] private RuntimeAnimatorController idleAnimatorController;
    [SerializeField] private RuntimeAnimatorController runAnimatorController;

    [SerializeField] public BoxCollider2D detectionCollider;

    public Vector3 initialPosition;
    [SerializeField] private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private bool hasReachedTarget = false; // State variable
    private GameObject enemyTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialPosition = EtherealWarriorGameObject.transform.position;
    }

    private void Update()
    {
        if (EtherealWarriorScript.enemyList.Count > 0)
        {
            // Remove null or dead enemies from the list
            for (int i = EtherealWarriorScript.enemyList.Count - 1; i >= 0; i--)
            {
                GameObject enemy = EtherealWarriorScript.enemyList[i];
                if (enemy == null || enemy.GetComponent<EnemyController>().isDead)
                {
                    EtherealWarriorScript.enemyList.RemoveAt(i);
                }
            }

            if (EtherealWarriorScript.enemyList.Count > 0)
            {
                // Target the first alive skeleton in the list
                enemyTarget = EtherealWarriorScript.enemyList[0];

                if (!hasReachedTarget)
                {
                    // Move towards the target skeleton
                    MoveTowardsTarget(enemyTarget);
                }
                else
                {
                    // Move to the right after reaching the target
                    MoveToRightOfTarget(enemyTarget);
                }
            }
            else
            {
                // Stop the projectile if there are no targets
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            // Stop the projectile if there are no targets
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveTowardsTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Use Rigidbody position for the distance check
        if (Vector3.Distance(rb.position, target.transform.position) < 0.5f)
        {
            hasReachedTarget = true; // Set flag to true when target is reached
            Debug.Log("Reached the target man");
        }
    }

    private void MoveToRightOfTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position + new Vector3(rightMovementOffset, 0f, 0f) - transform.position).normalized;
        rb.velocity = direction * speed;

        if (Vector3.Distance(rb.position, target.transform.position + new Vector3(rightMovementOffset, 0f, 0f)) < 0.5f)
        {
            Debug.Log("Reached the right of the target.");
            hasReachedTarget = false;  // Reset state to start moving toward the target again
        }
    }

    private void FixedUpdate()
    {
        FlipCharacterSprite();
    }

    private void FlipCharacterSprite()
    {
        if (rb.velocity.x > 0)
        {
            anim.runtimeAnimatorController = runAnimatorController;
            transform.localScale = new Vector3(4f, 4f, 4f);
        }

        if (rb.velocity.x < 0)
        {
            anim.runtimeAnimatorController = runAnimatorController;
            transform.localScale = new Vector3(-4f, 4f, 4f);
        }

        if (rb.velocity.x == 0)
        {
            anim.runtimeAnimatorController = idleAnimatorController;
        }
    }
}