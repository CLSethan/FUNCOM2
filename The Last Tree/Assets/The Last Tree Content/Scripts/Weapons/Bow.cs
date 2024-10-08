using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;  // The projectile to shoot
    [SerializeField] private Transform firePoint;          // The point where the arrow is shot from
    [SerializeField] private float fireRate = 1f;          // The rate of fire (shots per second)
    [SerializeField] private float projectileSpeed = 10f;  // Speed of the projectile

    private float nextFireTime = 0f;                       // Time until next shot

    void Update()
    {
        // Check if the fire button is pressed and if enough time has passed to fire again
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;  // Update the next time the bow can fire
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = firePoint.right * projectileSpeed;

        Debug.Log("THE BOW IS BEING SHOT");
    }
}
