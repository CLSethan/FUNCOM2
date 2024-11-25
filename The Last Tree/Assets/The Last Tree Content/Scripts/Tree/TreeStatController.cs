using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TreeStatController : MonoBehaviour
{

    public static TreeStatController treeInstance;
    [Header("References")]
    public Tree tree;
    public TreeHealth treeHealth;

    public List<TreeStatValue> weaponDamage, healthRegen, damageReduction;
    public int weaponDamageLevelCount, healthRegenLevelCount, damageReductionLevelCount;
    public int weaponDamageLevel, healthRegenLevel, damageReductionLevel;

    /*public GameObject treeSpiritSpawner;*/

    private void Awake()
    {
        treeInstance = this;
        /*treeSpiritSpawner.SetActive(false);*/

    }

    private void Start()
    {
        //player = FindFirstObjectByType<Player>();
        treeHealth = tree.GetComponent<TreeHealth>();
        //add values for each level of stat value
        for (int i = weaponDamage.Count - 1; i < weaponDamageLevelCount; i++)
        {
            weaponDamage.Add(new TreeStatValue(weaponDamage[i].cost + weaponDamage[1].cost, weaponDamage[i].value + (weaponDamage[1].value - weaponDamage[0].value)));
        }

        for (int i = healthRegen.Count - 1; i < healthRegenLevelCount; i++)
        {
            healthRegen.Add(new TreeStatValue(healthRegen[i].cost + healthRegen[1].cost, healthRegen[i].value + (healthRegen[1].value - healthRegen[0].value)));
        }

        for (int i = damageReduction.Count - 1; i < damageReductionLevelCount; i++)
        {
            damageReduction.Add(new TreeStatValue(damageReduction[i].cost + damageReduction[1].cost, damageReduction[i].value + (damageReduction[1].value - damageReduction[0].value)));
        }
    }


    public void UpdateUI()
    {
        if (weaponDamageLevel < weaponDamage.Count - 1)
        {
            UIController.Instance.moveSpeedUpgradeUI.UpdateDisplay(weaponDamage[weaponDamageLevel + 1].cost, weaponDamage[weaponDamageLevel].value, weaponDamage[weaponDamageLevel + 1].value);

        }
        else
        {
            UIController.Instance.moveSpeedUpgradeUI.ShowMaxLevel();
        }

        if (healthRegenLevel < healthRegen.Count - 1)
        {
            UIController.Instance.healthUpgradeUI.UpdateDisplay(healthRegen[healthRegenLevel + 1].cost, healthRegen[healthRegenLevel].value, healthRegen[healthRegenLevel + 1].value);

        }
        else
        {
            UIController.Instance.healthUpgradeUI.ShowMaxLevel();
        }

        if (damageReductionLevel < damageReduction.Count - 1)
        {
            UIController.Instance.pickupRangeUpgradeUI.UpdateDisplay(damageReduction[damageReductionLevel + 1].cost, damageReduction[damageReductionLevel].value, damageReduction[damageReductionLevel + 1].value);

        }
        else
        {
            UIController.Instance.pickupRangeUpgradeUI.ShowMaxLevel();
        }
    }

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
        treeHealth.maxHealth = healthRegen[healthRegenLevel].value;
        treeHealth.currentHealth += healthRegen[healthRegenLevel].value - healthRegen[healthRegenLevel - 1].value;
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
public class TreeStatValue
{
    public int cost;
    public float value;

    public TreeStatValue(int newCost, float newValue)
    {
        cost = newCost;
        value = newValue;
    }
}
