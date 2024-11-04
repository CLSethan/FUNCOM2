using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerExperience : MonoBehaviour
{
    public int currentPlayerExperience = 0;
    public int maxPlayerExperience = 50;
    public Slider xpSlider;

    [SerializeField] private GameObject UpgradeMenu;

    public void Start()
    {
        xpSlider.value = currentPlayerExperience;
        xpSlider.maxValue = maxPlayerExperience;

    }
    public void AddExperience(int amount)
    {
        currentPlayerExperience += amount;

        // Ensure experience doesn't exceed the max value
        if (currentPlayerExperience >= maxPlayerExperience)
        {
            currentPlayerExperience = maxPlayerExperience;
            LevelUp(); // Call level-up or any other function
        }

        xpSlider.value = currentPlayerExperience;

    }

    private void LevelUp()
    {
        UIController.Instance.ShowUpgradeMenu();
        PlayerStatController.instance.UpdateUI();
        currentPlayerExperience = 0;
        maxPlayerExperience += 50;
        Debug.Log("Player leveled up!");
        // Add logic for what happens when the player levels up
    }
}

