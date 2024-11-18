using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public PlayerStatUpgradeUI moveSpeedUpgradeUI, healthUpgradeUI, pickupRangeUpgradeUI;
    public GameObject UpgradeMenu;

    public TMP_Text coinText;

    private void Awake()
    {
        Instance = this;
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
        UpgradeMenu.SetActive(true);
        GameManager.Instance.WeaponUpgradeMenu.GenerateRandomWeaponUpgrades();
        Time.timeScale = 0;
    }

    public void HideUpgradeMenu()
    {
        //moved from player experience
        Time.timeScale = 1;
        GameManager.Instance.WeaponManager.isUpgradeMenuActive = false;
        /*GameManager.Instance.WeaponUpgradeMenu.DestroyWeaponUpgradeInstances();*/
        UpgradeMenu.SetActive(false);
    }

    public void UpdateCoins()
    {
        coinText.text = "Seeds: " + CoinController.instance.currentCoins;
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
