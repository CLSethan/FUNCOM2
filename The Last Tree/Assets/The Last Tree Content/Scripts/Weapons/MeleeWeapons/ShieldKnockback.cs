using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldKnockback : MonoBehaviour
{

    [SerializeField] private float knockbackForce; // The strength of the knockback
    [SerializeField] private float knockbackLength; 
    private bool isEvolved;

    
    Vector2 knockbackDir;

    // Start is called before the first frame update
    void Start()
    {
        knockbackForce = 0f;
        knockbackLength = 0f;
        isEvolved = false;
        fixLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Enemy") && other.gameObject.tag != "Player")
        {
            Rigidbody2D enemyRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                // Allows locking enemy's movement.
                EnemyController _enemyController = other.gameObject.GetComponent<EnemyController>();

                if (isEvolved)
                {
                    _enemyController.StartCoroutine(_enemyController.movementStunLock(knockbackLength, true));
                }
                else if (!isEvolved)
                {
                    _enemyController.StartCoroutine(_enemyController.movementStunLock(knockbackLength, true));
                }

                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                Vector2 force = knockbackDirection * knockbackForce;

                enemyRigidbody.AddForce(force, ForceMode2D.Impulse);

                knockbackDir = knockbackDirection;
            }
        }
    }

    public void fixLevel(int currentLevel)
    {
        switch(currentLevel)
        {
            case 1: 
            knockbackForce = 6f;
            knockbackLength = 0.2f;
            break;

            case 2:
            knockbackForce = 7f;
            knockbackLength = 0.2f;
            break;

            case 3:
            knockbackForce = 8f;
            knockbackLength = 0.24f;
            break;

            case 4:
            knockbackForce = 9f;
            knockbackLength = 0.25f;
            break;

            case 5:
            knockbackForce = 11f;
            knockbackLength = 0.27f;
            break;

            case 6:
            knockbackForce = 13f;
            knockbackLength = 0.32f;
            break;

            case 7:
            knockbackForce = 9f;
            knockbackLength = 0.25f;
            isEvolved = true;
            break;
        }
    }


    // private void OnDrawGizmos()
    // {
    // Gizmos.color = Color.red;
    // Gizmos.DrawLine(transform.position, transform.position + (Vector3)(knockbackDir * knockbackForce));
    // }


}
