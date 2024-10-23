using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour, IUpgradeableWeapon
{
    private int _currentUpgradeLevel;
    private int _upgradeLevelMax;

    public int currentUpgradeLevel
    {
        get { return _currentUpgradeLevel; }
        private set { _currentUpgradeLevel = Mathf.Clamp(value, 0, upgradeLevelMax); } // Ensure it doesn't exceed the max level
    }

    public int upgradeLevelMax
    {
        get { return _upgradeLevelMax; }
        private set { _upgradeLevelMax = Mathf.Max(value, 0); } // Ensure max level is positive
    }

    /*    [SerializeField] private WeaponManager weaponManager;*/

    [SerializeField] private GameObject projectilePrefab;  // The projectile to shoot
    [SerializeField] private Transform firePoint;          // The point where the arrow is shot from
    [SerializeField] public float currentFireRate = 1.1f; // The rate of fire (shots per second)
    [SerializeField] public float fireRateMax = 0.5f;
    [SerializeField] private float projectileSpeed = 10f;  // Speed of the projectile

    [SerializeField] bool canShoot = true;

    private Vector3 projectileScaleMultiplier = Vector3.one; //sets the vector3 to (1, 1, 1)

    private float nextFireTime = 0f;                       // Time until next shot

    private void Awake()
    {
        WeaponManager.Instance.CrossbowWeapon = this;

        currentUpgradeLevel = 0;
        upgradeLevelMax = 6;
    }

    void Start()
    {
    }

    void Update()
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

    private IEnumerator ReloadTime(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);

        canShoot = true;
    }

    void Shoot(Vector2 direction)
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

    public void Evolve()
    {
        // Increase the scale multiplier for future projectiles
        projectileScaleMultiplier += new Vector3(1f, 1f, 0f); // Increase scale on x and y axes
        Debug.Log($"Projectile scale increased to: {projectileScaleMultiplier}");
        Debug.Log("Bow has evolved");
    }

    public void Upgrade()
    {
        currentFireRate = Mathf.Max(currentFireRate - 0.1f, fireRateMax);
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
}
