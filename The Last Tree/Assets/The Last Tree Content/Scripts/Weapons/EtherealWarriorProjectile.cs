using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EtherealWarriorProjectile : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private EtherealWarrior EtherealWarriorScript;
    [SerializeField] private GameObject EtherealWarriorGameObject;

    // Assignable collider that will detect skeleton targets
    [SerializeField] public BoxCollider2D detectionCollider;

    public Vector3 initialPosition;

    private void Awake()
    {
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
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    private void HandleTargetReached()
    {
        // Logic for what happens when the projectile reaches its target
        // Example: Destroy the projectile or apply damage
        Destroy(gameObject);  // Destroy the projectile
    }
}