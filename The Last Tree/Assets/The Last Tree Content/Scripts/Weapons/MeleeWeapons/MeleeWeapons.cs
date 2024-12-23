using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IUpgradeableWeapon
{
    [SerializeField] private int _currentUpgradeLevel;
    [SerializeField] protected int _upgradeLevelMax;
    
    public int currentUpgradeLevel
    {
        get { return _currentUpgradeLevel; }
        set { _currentUpgradeLevel = Mathf.Clamp(value, 0, upgradeLevelMax); } // Ensure it doesn't exceed the max level
    }

    public int upgradeLevelMax
    {
        get { return _upgradeLevelMax; }
        set { _upgradeLevelMax = Mathf.Max(value, 0); } // Ensure max level is positive
    }

    /*    [SerializeField] private WeaponManager weaponManager;*/

    [SerializeField] protected float currentAttackRate; // The speed of swinging
    [SerializeField] protected float maxAttackRate;
    [SerializeField] protected bool canAttack = true;

    protected virtual IEnumerator ReloadTime(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);

        canAttack = true;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void TriggerAttack()
    {
        Debug.Log(this.gameObject.name + ": swish swash");
    }

    public virtual void Evolve()
    {
        Debug.Log("Melee weapon " + this.gameObject.name + " has evolved");
    }

    public virtual void Upgrade()
    {
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }

    public virtual void ResetWeapon()
    {
        currentUpgradeLevel = 0;
        canAttack = true;
    }
}
