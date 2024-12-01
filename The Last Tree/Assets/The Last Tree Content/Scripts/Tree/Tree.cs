using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public static Tree instance;

    public float weaponDamage, healthRegen, damageReduction;

    private void Awake()
    {
        instance = this;

        weaponDamage = 0;
        healthRegen = 0;
        damageReduction = 1;
    }

    public void ResetTreeStats()
    {
        weaponDamage = 0;
        healthRegen = 0;
        damageReduction = 1;
    }
}
