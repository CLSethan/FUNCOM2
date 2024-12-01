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

    private bool hasStartedGame = false;

    private void Awake()
    {
        GameManager.Instance.PlayerExperience = this;
    }

    public void Start()
    {
        xpSlider.value = currentPlayerExperience;
        xpSlider.maxValue = maxPlayerExperience;

        LevelUp();

    }

    private void Update()
    {
        if (!hasStartedGame)
        {
            LevelUp();
            hasStartedGame = true; // Disable further checks
        }
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
        xpSlider.maxValue = maxPlayerExperience;
        Debug.Log("Player leveled up!");
        // Add logic for what happens when the player levels up
    }

    public void ResetPlayerExperience()
    {
        currentPlayerExperience = 0;
        maxPlayerExperience = 50;
        xpSlider.value = currentPlayerExperience;
        xpSlider.maxValue = maxPlayerExperience;
        hasStartedGame = false;
    }
}

