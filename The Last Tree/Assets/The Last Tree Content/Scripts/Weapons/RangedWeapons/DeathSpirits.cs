using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpirits : RangedWeapon
{
    [SerializeField] public float currentActiveTime;
    [SerializeField] private float maxActiveTime;

    private void Awake()
    {
        WeaponManager.Instance.DeathSpiritsWeapon = this;
        currentUpgradeLevel = 0;
        upgradeLevelMax = 3;
        currentFireRate = 11f; // The rate of fire (shots per second)
        currentActiveTime = 4f;
        maxActiveTime = 7f;
        fireRateMax = 8f;
    }

    protected override void Update()
    {
        if (canShoot)
        {
            SpawnDeathSpirits();

            StartCoroutine(ReloadTime(currentFireRate));
        }
    }

    private void SpawnDeathSpirits()
    {
        Instantiate(projectilePrefab, firePoint.position + new Vector3(2f, 2f, 0f), Quaternion.Euler(0, 0, 0));

        Instantiate(projectilePrefab, firePoint.position + new Vector3(2f, -2f, 0f), Quaternion.Euler(0, 0, 0));

        Instantiate(projectilePrefab, firePoint.position + new Vector3(-4f, -2f, 0f), Quaternion.Euler(0, 0, 0));

        Instantiate(projectilePrefab, firePoint.position + new Vector3(-4f, 2f, 0f), Quaternion.Euler(0, 0, 0));

        canShoot = false;
    }

    public override void Evolve()
    {
        currentActiveTime = Mathf.Min(currentActiveTime + 4f, maxActiveTime);
        currentFireRate = Mathf.Max(currentFireRate - 4f, fireRateMax);
        Debug.Log("Death Spirits have evolved");
    }

    public override void Upgrade()
    {
        currentActiveTime = Mathf.Min(currentActiveTime + 1f, maxActiveTime);
        currentFireRate = Mathf.Max(currentFireRate - 1f, fireRateMax);
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
}
