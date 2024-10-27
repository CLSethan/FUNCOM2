using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public bool isEvolvedArrow = false;
    public bool isMeleeWeapon = false;
    public bool isShield = false;

    public float GetDamage()
    {
        return damage;
    }

    public void OnHit()
    {
        if (!isEvolvedArrow && !isMeleeWeapon)
        {
            Destroy(gameObject); // Destroy the projectile only if it's not an evolved arrow or sword
        }
        else
        { 
        }
    }

    // Checks if the shield is equipped, and if it is, decreases the mass of the detected opponent in order for it to get flung further away.
    // WARNING -- BUGGED AT THE MOMENT, NOT FUNCTIONAL. STILL FIGURING OUT THE COLLISIONS 
    public void OnCollisionEnter2D(Collision2D other) 
    {
        {
            if (isShield)
            {
                if (other.gameObject.name == "Skeleton" || other.gameObject.name == "Goblin" )
                    {
                        Rigidbody2D enemyBody = other.gameObject.GetComponent<Rigidbody2D>();
                        shieldKnockback(enemyBody);
                        Debug.Log(other.gameObject);
                    }
            }
        }
    }

    private IEnumerator shieldKnockback(Rigidbody2D opponent)
    {
        float scaleDuration = 0.8f;
        float elapsedTime = 0f;
        Debug.Log(opponent.mass);
        while (elapsedTime < scaleDuration)
        {
            opponent.mass = 0.6f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log(opponent.mass);
        elapsedTime = 0f;
            
        while (elapsedTime < scaleDuration)
        {
            opponent.mass = 1f;
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        Debug.Log(opponent.mass);
    }
}
