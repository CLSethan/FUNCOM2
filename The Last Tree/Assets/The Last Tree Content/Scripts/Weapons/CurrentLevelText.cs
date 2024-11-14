using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponTypeText
{
    BOW_TEXT,
    CROSSBOW_TEXT,
    ETHEREAL_WARRIOR_TEXT,
    SWORD_TEXT,
    SHIELD_TEXT,
    FIRE_ORB_TEXT,
    DEATH_SPIRITS_TEXT
}

public class CurrentLevelText : MonoBehaviour
{
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text maxLevelText;

    [SerializeField] private WeaponTypeText weaponTypeText;

    // Start is called before the first frame update
    void Start()
    {
        switch (weaponTypeText)
        {
            case WeaponTypeText.BOW_TEXT:
                currentLevelText.text = "Current Level: " + WeaponManager.Instance.BowWeapon.currentUpgradeLevel;
                maxLevelText.text = "Max Level: " + WeaponManager.Instance.BowWeapon.upgradeLevelMax;
                break;
            case WeaponTypeText.CROSSBOW_TEXT:
                currentLevelText.text = "Current Level: " + WeaponManager.Instance.CrossbowWeapon.currentUpgradeLevel;
                maxLevelText.text = "Max Level: " + WeaponManager.Instance.CrossbowWeapon.upgradeLevelMax;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
