using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IUpgradeableWeapon
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

    [SerializeField] protected GameObject projectilePrefab;  // The projectile to shoot
    [SerializeField] protected Transform firePoint;          // The point where the arrow is shot from
    [SerializeField] protected float currentFireRate; // The rate of fire (shots per second)
    [SerializeField] protected float fireRateMax;
    [SerializeField] protected float projectileSpeed;  // Speed of the projectile

    [SerializeField] protected bool canShoot = true;

    private float nextFireTime = 0f;                       // Time until next shot

    protected virtual void Start()
    {
        Debug.Log("currentFireRate: " + currentFireRate);
        Debug.Log("fireRateMax: " + fireRateMax);
        Debug.Log("projectileSpeed: " + projectileSpeed);
    }

    protected virtual void Update()
    {
    }

    protected virtual IEnumerator ReloadTime(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);

        canShoot = true;
    }

    protected virtual void Shoot(Vector2 direction)
    {
        Debug.Log(this.gameObject.name + ": Pew Pew");
    }

    public virtual void Evolve()
    {
        Debug.Log("Bow has evolved");
    }

    public virtual void Upgrade()
    {
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
}
