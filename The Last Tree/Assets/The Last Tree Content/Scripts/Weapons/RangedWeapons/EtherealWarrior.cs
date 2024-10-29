using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealWarrior : RangedWeapon
{
    [SerializeField] private BoxCollider2D WeaponCollider;
    [SerializeField] public List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] private GameObject Projectile;
    [SerializeField] private EtherealWarriorProjectile EtherealWarriorProjectileScript;

    [SerializeField] public bool isAvailable = true;
    [SerializeField] public bool hasActivated = true;
    [SerializeField] private bool isEvolved = false;
    [SerializeField] private float CooldownTime = 3f;
    [SerializeField] public float maxKillTime = 3f;

    private bool isCooldownActive = false;
    private bool isActiveTimeActive = false;

    private void Awake()
    {
        WeaponManager.Instance.EtherealWarriorWeapon = this;
        currentUpgradeLevel = 0;
        upgradeLevelMax = 8;
        currentFireRate = 3f; // The rate of fire (shots per second)
        fireRateMax = 3f;
        projectileSpeed = 10f;  // Speed of the projectile
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isEvolved)
        {
            if (!Projectile.activeInHierarchy)  // Check if it's already active
            {
                Projectile.SetActive(true);
                Debug.Log("THE WARRIOR IS NOW IMMORTAL");
            }
        }
        else
        {
            if (isAvailable)
            {
                Projectile.SetActive(true);
                StartCoroutine(ActiveTime());
            }
            else
            {
                Projectile.SetActive(false);
                StartCoroutine(Cooldown());
            }
        }
    }

    private void Shoot()
    {
        if (enemyList.Count <= 0)
        {
            Projectile.transform.position = this.transform.position;
        }
        else
        {
            /*Debug.Log("There's elements in the list");*/
        }

        /*Debug.Log("Ethereal Warrior is out");*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object contains "Skeleton" in its name (Should change this to Enemy instead)
        if (other.gameObject.name.Contains("Enemy"))
        {
            Shoot();

            // Add the skeleton to the list of targets if not already in the list
            if (!enemyList.Contains(other.gameObject))
            {
                enemyList.Add(other.gameObject);
            }
        }
    }

    private IEnumerator Cooldown()
    {
        Projectile.transform.position = this.transform.position;

        if (isCooldownActive || isActiveTimeActive)
            yield break; // Avoid multiple instances of the coroutine

        isCooldownActive = true;

        yield return new WaitForSeconds(CooldownTime);

        isAvailable = true;
        isCooldownActive = false;

        Debug.Log("THE WARRIOR HAS COME BACK TO KILL AGAIN");
    }

    private IEnumerator ActiveTime()
    {
        if (isActiveTimeActive || isCooldownActive)
            yield break; // Avoid multiple instances of the coroutine

        isActiveTimeActive = true;

        yield return new WaitForSeconds(maxKillTime);

        isAvailable = false;
        isActiveTimeActive = false;
    }

    public override void Evolve()
    {
        EtherealWarriorProjectileScript.speed += 2f;
        isEvolved = true;
        Debug.Log("The Queen has Arrived");
    }

    public override void Upgrade()
    {
        maxKillTime += 2f;
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
}
