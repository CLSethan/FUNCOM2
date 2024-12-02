using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUpgradeUI : MonoBehaviour
{
    public Text valueText, costText;

    public Button upgradeButton;

    public void UpdateDisplay(int cost, float oldValue, float newValue)
    {
        valueText.text = "Value: " + oldValue.ToString("F1") + " -> " + newValue.ToString("F1");
        costText.text = "Cost: " + cost.ToString();

        if(cost <= CoinController.instance.currentCoins)
        {
         //   upgradeButton.SetActive(true);
            upgradeButton.interactable = true;
        }
        else
        {
           // upgradeButton.SetActive(false);
            upgradeButton.interactable = false;

        }
    }

    public void ShowMaxLevel()
    {
        valueText.text = "THIS IS YOUR LIMIT!";
        costText.text = "THIS IS YOUR LIMIT!";
       // upgradeButton.SetActive(false);
        upgradeButton.interactable = false;


    }
}
