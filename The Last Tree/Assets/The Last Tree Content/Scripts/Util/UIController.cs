using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private EnemyController enemyController;
    public static UIController Instance;
    public PlayerStatUpgradeUI moveSpeedUpgradeUI, healthUpgradeUI, pickupRangeUpgradeUI;
    public GameObject WeaponUpgradeMenu;
    public GameObject PlayerUpgradeMenu;

    public TMP_Text coinText;
    public TMP_Text enemyText; //Added text to count kills

    public bool playerCanUpgrade;

    private void Awake()
    {
        Instance = this;
        WeaponUpgradeMenu.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowUpgradeMenu()
    {
        // moved from weaponmanager
        GameManager.Instance.WeaponManager.isUpgradeMenuActive = true;
        WeaponUpgradeMenu.SetActive(true);
        GameManager.Instance.WeaponUpgradeMenu.GenerateRandomWeaponUpgrades();
        Time.timeScale = 0;
    }

    public void HideUpgradeMenu()
    {
        //moved from player experience
        Time.timeScale = 1;
        GameManager.Instance.WeaponManager.isUpgradeMenuActive = false;
       // GameManager.Instance.WeaponUpgradeMenu.DestroyWeaponUpgradeInstances();
        WeaponUpgradeMenu.SetActive(false);
    }

    public void ShowPlayerUpgradeMenu()
    {
        if (CoinController.instance.currentCoins <= 5)
        {
            return;
        }

        Time.timeScale = 0;
        PlayerUpgradeMenu.SetActive(true);
    }

    public void UpdateCoins()
    {
        coinText.text = "Seeds: " + CoinController.instance.currentCoins;
    }

    public void UpdateKills()
    {
        enemyText.text = "Kills: " + enemyController.enemiesKilled;
    }

    public void ClosePlayerUpgradeMenu()
    {
        Time.timeScale = 1;
        PlayerUpgradeMenu.SetActive(false);
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
    }
    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();

    }
    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();

    }
}
