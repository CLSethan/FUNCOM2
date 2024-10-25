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

    [SerializeField] protected float currentSwingRate; // The speed of swinging
    [SerializeField] protected float maxSwingRate;

    [SerializeField] protected bool canSwing = true;

    protected virtual void Start()
    {
        Debug.Log("currentSwingRate: " + currentSwingRate);
        Debug.Log("maxSwingRate: " + maxSwingRate);
    }

    protected virtual void Update()
    {
    }

    protected virtual IEnumerator ReloadTime(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);

        canSwing = true;
    }

    protected virtual void Swing(Vector2 direction)
    {
        Debug.Log(this.gameObject.name + ": swish swash");
    }

    public virtual void Evolve()
    {
        Debug.Log("Sword has evolved");
    }

    public virtual void Upgrade()
    {
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
}
