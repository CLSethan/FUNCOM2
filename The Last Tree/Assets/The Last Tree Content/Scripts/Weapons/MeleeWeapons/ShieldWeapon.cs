using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : MeleeWeapon
{
    private Vector3 shieldScaleMultiplier = Vector3.zero;
    [SerializeField] private GameObject shieldGameObject;
    [SerializeField] private float scaleSpeed = 0.05f;
    [SerializeField] private Vector3 maxScale = new Vector3(1.5f, 1.5f, 1f);
    [SerializeField] private bool isExpanding = false;
    [SerializeField] private bool isShrinking = false;
    [SerializeField] private bool isOnCooldown = false;
    [SerializeField] private float maxShieldSize = 7f;
    [SerializeField] private float shieldUpgradeSizeAmount = 0.5f;

    private void Awake()
    {
        WeaponManager.Instance.ShieldWeapon = this;
        shieldGameObject = this.gameObject;
        shieldGameObject.transform.localScale = shieldScaleMultiplier;
        currentUpgradeLevel = 0;
        upgradeLevelMax = 6;
    }

    protected override void Update()
    {
        if (canAttack)
        {
            StartCoroutine(ExpandShield());
            canAttack = false;
        }
    }

    private IEnumerator ExpandShield()
    {
        if (isExpanding || isShrinking || isOnCooldown)
            yield break;

        isExpanding = true;

        while (shieldGameObject.transform.localScale.x < maxScale.x)
        {
            shieldGameObject.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            yield return null;
        }

        isExpanding = false;

        StartCoroutine(ShrinkShield());
    }

    private IEnumerator ShrinkShield()
    {
        if (isExpanding || isShrinking || isOnCooldown)
            yield break;

        isShrinking = true;

        while (shieldGameObject.transform.localScale.x > 0)
        {
            shieldGameObject.transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            yield return null;
        }

        isShrinking = false;

        StartCoroutine(Cooldown(currentAttackRate));
    }

    private IEnumerator Cooldown (float reloadTime)
    {
        if (isExpanding || isShrinking || isOnCooldown)
            yield break;

        isOnCooldown = true;

        yield return new WaitForSeconds(reloadTime);

        canAttack = true;
        isOnCooldown = false;
    }

    public override void Evolve()
    {
        Debug.Log("Melee weapon " + this.gameObject.name + " has evolved");
        maxScale.x = Mathf.Min(maxScale.x + maxShieldSize, maxShieldSize);
    }

    public override void Upgrade()
    {
        Debug.Log("This shield is upgrading");
        maxScale.x = Mathf.Min(maxScale.x + shieldUpgradeSizeAmount, maxShieldSize);
        base.Upgrade();
    }
}
