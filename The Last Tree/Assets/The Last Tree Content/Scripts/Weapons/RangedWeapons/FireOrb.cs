using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrb : RangedWeapon
{
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        WeaponManager.Instance.FireOrbWeapon = this;
        currentUpgradeLevel = 0;
        upgradeLevelMax = 5;
        currentFireRate = 1.5f; // The rate of fire (shots per second)
        fireRateMax = 0.1f;
        projectileSpeed = 8f;  // Speed of the projectile
    }

    protected override void Update()
    {
        RemoveDeadEnemies(); // Clean up any null references in the enemy list

        if (enemyList.Count > 0)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null && canShoot)
            {
                Shoot(nearestEnemy);
                StartCoroutine(ReloadTime(currentFireRate));
            }
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemyList)
        {
            float distance = Vector2.Distance(firePoint.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void Shoot(GameObject target)
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Calculate the direction from firePoint to target
        Vector2 direction = (target.transform.position - firePoint.position).normalized;

        // Set the projectile's velocity toward the target
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        canShoot = false;
    }

    private void RemoveDeadEnemies()
    {
        enemyList.RemoveAll(enemy => enemy == null || enemy.GetComponent<EnemyController>().isDead); // Remove any null elements from enemyList
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object contains "Enemy" in its name (Should change this to Enemy instead)
        if (other.gameObject.name.Contains("Enemy"))
        {
            // Add the skeleton to the list of targets if not already in the list
            if (!enemyList.Contains(other.gameObject))
            {
                enemyList.Add(other.gameObject);
            }
        }
    }

    public override void Evolve()
    {
        currentFireRate = Mathf.Max(currentFireRate - 5f, fireRateMax);
        Debug.Log("Auto Orbs incoming");
    }

    public override void Upgrade()
    {
        currentFireRate = Mathf.Max(currentFireRate - 0.2f, fireRateMax);
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }

    public override void ResetWeapon()
    {
        base.ResetWeapon();

        currentFireRate = 1.5f;
        projectileSpeed = 8f;
    }
}