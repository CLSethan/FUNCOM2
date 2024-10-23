using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : RangedWeapon
{
    [SerializeField] private GameObject evolvedProjectilePrefab;  // The projectile to shoot

    [SerializeField] private float currentSpreadAngle = 15f;  // Angle between each projectile in the spread
    [SerializeField] private float maxSpreadAngle = 105f;  // Angle between each projectile in the spread
    [SerializeField] private int currentNumProjectiles = 3;   // Number of projectiles to shoot in the spread
    [SerializeField] private int numProjectilesMax = 10;   // Number of projectiles to shoot in the spread
    Vector2 adjustedDirection;

    private void Awake()
    {
        WeaponManager.Instance.BowWeapon = this;

        InitCrossbowData();
    }

    private void InitCrossbowData()
    {
        currentUpgradeLevel = 0;
        upgradeLevelMax = 6;
        currentFireRate = 1.1f; // The rate of fire (shots per second)
        fireRateMax = 0.5f;
        projectileSpeed = 10f;  // Speed of the projectile
    }

    // Update is called once per frame
    protected override void Update()
    {
        adjustedDirection = firePoint.right; // Right points in the direction the fire point is facing
        adjustedDirection.Normalize();  // Normalize to keep it a unit vector

        if (canShoot)
        {
            ShootSpread(adjustedDirection, currentNumProjectiles, currentSpreadAngle);

            StartCoroutine(ReloadTime(currentFireRate));
        }
    }

    protected override IEnumerator ReloadTime(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);

        canShoot = true;
    }

    protected override void Shoot(Vector2 direction)
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;

        canShoot = false;
    }

    private void ShootSpread(Vector2 shootDirection, int projectileCount, float spreadAngle)
    {
        float angleStep = spreadAngle / (projectileCount - 1); // Step between each projectile's angle
        float startAngle = -spreadAngle / 2;                   // Start at half negative spread angle

        for (int i = 0; i < projectileCount; i++)
        {
            // Calculate the rotation angle for each projectile
            float angle = startAngle + i * angleStep;
            Vector2 spreadDirection = RotateVector(shootDirection, angle);

            Shoot(spreadDirection); // Shoot the projectile in the spread direction
        }
    }

    // Helper method to rotate a vector by an angle (in degrees)
    private Vector2 RotateVector(Vector2 originalVector, float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        float cosAngle = Mathf.Cos(angleRadians);
        float sinAngle = Mathf.Sin(angleRadians);

        return new Vector2(
            originalVector.x * cosAngle - originalVector.y * sinAngle,
            originalVector.x * sinAngle + originalVector.y * cosAngle
        );
    }

    public override void Evolve()
    {
        // Increase the scale multiplier for future projectiles
        projectilePrefab = evolvedProjectilePrefab;
        Debug.Log("Bow has evolved");
    }

    public override void Upgrade()
    {
        currentSpreadAngle = Mathf.Min(currentSpreadAngle + 15f, maxSpreadAngle);
        currentNumProjectiles = Mathf.Min(currentNumProjectiles + 1, numProjectilesMax);
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
}
