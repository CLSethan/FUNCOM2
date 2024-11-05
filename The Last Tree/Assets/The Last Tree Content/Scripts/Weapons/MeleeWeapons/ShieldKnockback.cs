using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldKnockback : MonoBehaviour
{

    public float knockbackForce = 10f; // The strength of the knockback

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
        
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.name.Contains("Enemy") && other.gameObject.tag != "Player")
    //     {
    //         Rigidbody2D _enemyRigidbody = other.gameObject.GetComponent<Rigidbody2D>();

    //         if (_enemyRigidbody != null)
    //         {
    //             Debug.Log("applying knockback");
    //             Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

    //             _enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    //         }
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Enemy") && other.gameObject.tag != "Player")
        {
            Rigidbody2D enemyRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Debug.Log(other.gameObject.name);
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
