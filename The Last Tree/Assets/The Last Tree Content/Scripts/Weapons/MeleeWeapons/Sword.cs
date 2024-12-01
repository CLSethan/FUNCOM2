using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Sword : MeleeWeapon
{
    [SerializeField] private float rotateRadius = 1;
    [SerializeField] private float angle;
    [SerializeField] private Transform player;
    [SerializeField] private UnityEngine.Vector3 initialScale;

    private void Awake()
    {
        WeaponManager.Instance.SwordWeapon = this;

        this.transform.localScale = new UnityEngine.Vector3(0.7f, 0.7f, 0.65f);
        currentUpgradeLevel = 0;
        upgradeLevelMax = 6;
        initialScale = this.transform.localScale;
        currentAttackRate = 4f;
        maxAttackRate = 8f;
    }
    protected override void Update()
    {
        // Update the angle over time
        angle += currentAttackRate * Time.deltaTime;

        // Calculate new position in a circular orbit around the player
        UnityEngine.Vector2 offset = new UnityEngine.Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * rotateRadius;
        transform.position = (UnityEngine.Vector2)player.position + offset;
    }

    public override void Upgrade()
    {
        currentAttackRate = Mathf.Min(currentAttackRate + 1f, maxAttackRate);
        transform.localScale += new  UnityEngine.Vector3(0.1f, 0.1f, 0.1f);
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }

        public override void Evolve()
    {
        // Increase the scale multiplier for future projectiles
        transform.localScale += new UnityEngine.Vector3(1f, 1f, 0f); // Increase scale on x and y axes
        Debug.Log($"Sword scale increased to: {transform.localScale}");
        Debug.Log("Sword has evolved");
    }

    public override void ResetWeapon()
    {
        base.ResetWeapon();

        currentAttackRate = 4f;
        this.transform.localScale = initialScale;
    }

}
