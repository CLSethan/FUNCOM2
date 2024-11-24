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

    public List<PlayerStatValue> moveSpeed, health, pickupRange;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
    public int moveSpeedLevel, healthLevel, pickupRangeLevel;
    public int 

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
        for(int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
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
