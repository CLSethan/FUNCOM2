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
}
