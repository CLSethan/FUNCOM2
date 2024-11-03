using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int currentPlayerExperience = 0;
    public int maxPlayerExperience = 50;
    [SerializeField] private GameObject UpgradeMenu;

    public void AddExperience(int amount)
    {
        currentPlayerExperience += amount;

        // Ensure experience doesn't exceed the max value
        if (currentPlayerExperience >= maxPlayerExperience)
        {
            currentPlayerExperience = maxPlayerExperience;
            LevelUp(); // Call level-up or any other function
        }
    }

    private void LevelUp()
    {
        GameManager.Instance.WeaponManager.isUpgradeMenuActive = true;
        UpgradeMenu.SetActive(true);
        currentPlayerExperience = 0;
        Time.timeScale = 0;
        Debug.Log("Player leveled up!");
        // Add logic for what happens when the player levels up
    }
}

