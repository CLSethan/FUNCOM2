using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EtherealWarriorProjectile : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private EtherealWarrior EtherealWarriorScript;
    [SerializeField] private GameObject EtherealWarriorGameObject;
    [SerializeField] private RuntimeAnimatorController idleAnimatorController;
    [SerializeField] private RuntimeAnimatorController runAnimatorController;

    // Assignable collider that will detect skeleton targets
    [SerializeField] public BoxCollider2D detectionCollider;

    public Vector3 initialPosition;
    [SerializeField]  private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Store the initial position when the projectile is instantiated
        initialPosition = EtherealWarriorGameObject.transform.position;
    }

    private void Update()
    {
        if (EtherealWarriorScript.enemyList.Count > 0)
        {
            // Directly check if the first element of the list is null
            if (EtherealWarriorScript.enemyList[0] == null)
            {
                EtherealWarriorScript.enemyList.RemoveAt(0);

                Debug.Log("Removed 1st element in enemyList");
            }
            else
            {
                // Target the first skeleton in the list
                GameObject enemyTarget = EtherealWarriorScript.enemyList[0];

                // Move towards the target skeleton
                Vector3 direction = (enemyTarget.transform.position - transform.position).normalized;

                // Use Rigidbody2D to move the projectile
                rb.velocity = direction * speed;
            }
        }
        else
        {
            // Stop the projectile if there are no targets
            rb.velocity = Vector2.zero;
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