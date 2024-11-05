using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldKnockback : MonoBehaviour
{

    public float knockbackForce = 10f; // The strength of the knockback
    public float knockbackDuration = 0.5f; // How long the knockback lasts

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
        
private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        // Check if the object we collided with has a Rigidbody2D
        if (other.gameObject.name == "EnemySkeleton" || other.gameObject.name == "EnemyGoblin" || other.gameObject.name == "EnemyFlyingEye" )
        {
            Rigidbody2D enemyRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                // Calculate the direction of the knockback
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                // Apply the knockback force
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
