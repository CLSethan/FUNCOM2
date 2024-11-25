using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{

    public static PlayerStatController instance;
    [Header("References")]
    public Player player;
    public PlayerHealth playerHealth;
    public PlayerController playerController;

    public Tree tree;

    public List<PlayerStatValue> moveSpeed, health, pickupRange, weaponDamage, healthRegen, damageReduction;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
    public int moveSpeedLevel, healthLevel, pickupRangeLevel;
    public int weaponDamageLevelCount, healthRegenLevelCount, damageReductionLevelCount;
    public int weaponDamageLevel, healthRegenLevel, damageReductionLevel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //player = FindFirstObjectByType<Player>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerController = player.GetComponent<PlayerController>();
        //add values for each level of stat value
        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }
        
        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }
        
        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }

        for (int i = weaponDamage.Count - 1; i < weaponDamageLevelCount; i++)
        {
            weaponDamage.Add(new PlayerStatValue(weaponDamage[i].cost + weaponDamage[1].cost, weaponDamage[i].value + (weaponDamage[1].value - weaponDamage[0].value)));
        }

        for (int i = healthRegen.Count - 1; i < healthRegenLevelCount; i++)
        {
            healthRegen.Add(new PlayerStatValue(healthRegen[i].cost + healthRegen[1].cost, healthRegen[i].value + (healthRegen[1].value - healthRegen[0].value)));
        }

        for (int i = damageReduction.Count - 1; i < damageReductionLevelCount; i++)
        {
            damageReduction.Add(new PlayerStatValue(damageReduction[i].cost + damageReduction[1].cost, damageReduction[i].value + (damageReduction[1].value - damageReduction[0].value)));
        }
    }


    public void UpdateUI()
    {
        if(moveSpeedLevel < moveSpeed.Count - 1)
        {
            UIController.Instance.moveSpeedUpgradeUI.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);

        }
        else
        {
            UIController.Instance.moveSpeedUpgradeUI.ShowMaxLevel();
        }

        if (healthLevel < health.Count - 1)
        {
            UIController.Instance.healthUpgradeUI.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);

        }
        else
        {
            UIController.Instance.healthUpgradeUI.ShowMaxLevel();
        }

        if (pickupRangeLevel < pickupRange.Count - 1)
        {
            UIController.Instance.pickupRangeUpgradeUI.UpdateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);

        }
        else
        {
            UIController.Instance.pickupRangeUpgradeUI.ShowMaxLevel();
        }

        //tree side
        if (weaponDamageLevel < weaponDamage.Count - 1)
        {
            UIController.Instance.weaponDamageUpgradeUI.UpdateDisplay(weaponDamage[weaponDamageLevel + 1].cost, weaponDamage[weaponDamageLevel].value, weaponDamage[weaponDamageLevel + 1].value);

        }
        else
        {
            UIController.Instance.weaponDamageUpgradeUI.ShowMaxLevel();
        }

        if (healthRegenLevel < healthRegen.Count - 1)
        {
            UIController.Instance.treeHealthRegenUpgradeUI.UpdateDisplay(healthRegen[healthRegenLevel + 1].cost, healthRegen[healthRegenLevel].value, healthRegen[healthRegenLevel + 1].value);

        }
        else
        {
            UIController.Instance.treeHealthRegenUpgradeUI.ShowMaxLevel();
        }

        if (damageReductionLevel < damageReduction.Count - 1)
        {
            UIController.Instance.damageReductionUpgradeUI.UpdateDisplay(damageReduction[damageReductionLevel + 1].cost, damageReduction[damageReductionLevel].value, damageReduction[damageReductionLevel + 1].value);

        }
        else
        {
            UIController.Instance.damageReductionUpgradeUI.ShowMaxLevel();
        }
    }

    public void PurchaseMoveSpeed()
    {
        moveSpeedLevel++;
        CoinController.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateUI();
        player.moveSpeed = moveSpeed[moveSpeedLevel].value;
        playerController.SetMoveSpeed();
    }
    public void PurchaseHealth()
    {
        healthLevel++;
        CoinController.instance.SpendCoins(health[healthLevel].cost);
        UpdateUI();
        playerHealth.maxHealth = health[healthLevel].value;
        playerHealth.currentHealth += health[healthLevel].value - health[healthLevel - 1].value;
    }
    public void PurchasePickupRange()
    {
        pickupRangeLevel++;
        CoinController.instance.SpendCoins(pickupRange[pickupRangeLevel].cost);;
        UpdateUI();
        player.pickupRange = pickupRange[pickupRangeLevel].value;
    }

    //tree
    public void PurchaseWeaponDamage()
    {
        /*        if (!treeSpiritSpawner.activeSelf)
                {
                    treeSpiritSpawner.SetActive(true);
                }*/

        weaponDamageLevel++;
        CoinController.instance.SpendCoins(weaponDamage[weaponDamageLevel].cost);
        UpdateUI();
        tree.weaponDamage = weaponDamage[weaponDamageLevel].value;
    }
    public void PurchaseTreeHealthRegen()
    {
        healthRegenLevel++;
        CoinController.instance.SpendCoins(healthRegen[healthRegenLevel].cost);
        UpdateUI();
        tree.healthRegen = healthRegen[healthRegenLevel].value;
    }
    public void PurchaseDamageReduction()
    {
        damageReductionLevel++;
        CoinController.instance.SpendCoins(damageReduction[damageReductionLevel].cost); ;
        UpdateUI();
        tree.damageReduction = damageReduction[damageReductionLevel].value;
    }
}

[System.Serializable]
public class PlayerStatValue
{
    public int cost;
    public float value;

    public PlayerStatValue(int newCost, float newValue)
    {
        cost = newCost;
        value = newValue;
    }
}
