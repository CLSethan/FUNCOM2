using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : RangedWeapon
{
    private Vector3 projectileScaleMultiplier = Vector3.one; //sets the vector3 to (1, 1, 1)

    private void Awake()
    {
        WeaponManager.Instance.CrossbowWeapon = this;
        currentUpgradeLevel = 0;
        upgradeLevelMax = 6;
        currentFireRate = 1.1f; // The rate of fire (shots per second)
        fireRateMax = 0.5f;
        projectileSpeed = 10f;  // Speed of the projectile
    }

    protected override void Update()
    {
        if (canShoot)
        {
            Shoot(firePoint.right);

            if (currentUpgradeLevel >= 2)
            {
                Shoot(-firePoint.right);
            }

            if (currentUpgradeLevel >= 4)
            {
                Shoot(firePoint.up);
            }

            if (currentUpgradeLevel >= 6)
            {
                Shoot(-firePoint.up);
            }

            StartCoroutine(ReloadTime(currentFireRate));
        }
    }

    protected override void Shoot(Vector2 direction)
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Rotate the projectile to face the direction of movement
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Set the projectile's velocity
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;

        // Adjust the scale of the projectile if needed
        projectile.transform.localScale = projectileScaleMultiplier;

        canShoot = false;
    }

    public override void Evolve()
    {
        // Increase the scale multiplier for future projectiles
        projectileScaleMultiplier += new Vector3(1f, 1f, 0f); // Increase scale on x and y axes
        Debug.Log($"Projectile scale increased to: {projectileScaleMultiplier}");
        Debug.Log("Bow has evolved");
    }

    public override void Upgrade()
    {
        currentFireRate = Mathf.Max(currentFireRate - 0.1f, fireRateMax);
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
}
