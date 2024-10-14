using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
/*    [SerializeField] private WeaponManager weaponManager;*/

    [SerializeField] private GameObject projectilePrefab;  // The projectile to shoot
    [SerializeField] private Transform firePoint;          // The point where the arrow is shot from
    [SerializeField] public float currentFireRate = 1.1f; // The rate of fire (shots per second)
    [SerializeField] public float fireRateMax = 0.5f;
    [SerializeField] public float currentUpgradeLevel = 0f;
    [SerializeField] public float upgradeLevelMax = 6f;
    [SerializeField] private float projectileSpeed = 10f;  // Speed of the projectile
    [SerializeField] private float projectileDestroyTime = 2.5f;

    [SerializeField] bool canShoot = true;

    private Vector3 projectileScaleMultiplier = Vector3.one; //sets the vector3 to (1, 1, 1)

    private float nextFireTime = 0f;                       // Time until next shot

    private void Awake()
    {
        WeaponManager.Instance.CrossbowWeapon = this;
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

    public IEnumerator ReloadTime(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);

        canShoot = true;
    }

    private IEnumerator DestroyProjectile(GameObject projectile)
    {
        yield return new WaitForSeconds(projectileDestroyTime);

        Destroy(projectile);
    }

    void Shoot(Vector2 direction)
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;

        projectile.transform.localScale = projectileScaleMultiplier;

        canShoot = false;

        StartCoroutine(DestroyProjectile(projectile));
    }

    public void EvolvedBow()
    {
        // Increase the scale multiplier for future projectiles
        projectileScaleMultiplier += new Vector3(1f, 1f, 0f); // Increase scale on x and y axes
        Debug.Log($"Projectile scale increased to: {projectileScaleMultiplier}");
        Debug.Log("Bow has evolved");
    }

    public void Upgrade()
    {
        currentFireRate = Mathf.Max(currentFireRate - 0.1f, fireRateMax);
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1f, upgradeLevelMax);
    }
}
