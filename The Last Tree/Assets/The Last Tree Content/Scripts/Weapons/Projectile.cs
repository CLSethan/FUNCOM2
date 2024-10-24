using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public bool isEvolvedArrow = false;

    public float GetDamage()
    {
        return damage;
    }

    public void OnHit()
    {
        // Add logic to handle what happens to the projectile on hit
        if (!isEvolvedArrow)
        {
            Destroy(gameObject); // Destroy the projectile only if it's not an evolved arrow
        }
        else
        {
        }
    }
}
