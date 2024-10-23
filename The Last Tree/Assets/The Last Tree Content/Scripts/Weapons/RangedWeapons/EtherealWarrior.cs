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
    protected override void Update()
    {
        if (isEvolved)
        {
            Projectile.SetActive(true);
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
            Debug.Log("There's elements in the list");
        }

        Debug.Log("Ethereal Warrior is out");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object contains "Skeleton" in its name (Should change this to Enemy instead)
        if (other.gameObject.name.Contains("Skeleton"))
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

        yield return new WaitForSeconds(CooldownTime);

        isAvailable = true;

        Debug.Log("THE WARRIOR HAS COME BACK TO KILL AGAIN");
    }

    private IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(maxKillTime);

        isAvailable = false;
    }

    public override void Evolve()
    {
        EtherealWarriorProjectileScript.speed += 1f;
        isEvolved = true;
    }

    public override void Upgrade()
    {
        maxKillTime += 2f;
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
}
