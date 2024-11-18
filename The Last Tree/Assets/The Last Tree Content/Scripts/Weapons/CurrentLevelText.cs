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
                DisplayLevels(currentLevelText, WeaponManager.Instance.BowWeapon.currentUpgradeLevel, maxLevelText, WeaponManager.Instance.BowWeapon.upgradeLevelMax);
                break;
            case WeaponTypeText.CROSSBOW_TEXT:
                DisplayLevels(currentLevelText, WeaponManager.Instance.CrossbowWeapon.currentUpgradeLevel, maxLevelText, WeaponManager.Instance.CrossbowWeapon.upgradeLevelMax);
                break;
            case WeaponTypeText.ETHEREAL_WARRIOR_TEXT:
                DisplayLevels(currentLevelText, WeaponManager.Instance.EtherealWarriorWeapon.currentUpgradeLevel, maxLevelText, WeaponManager.Instance.EtherealWarriorWeapon.upgradeLevelMax);
                break;
            case WeaponTypeText.SWORD_TEXT:
                DisplayLevels(currentLevelText, WeaponManager.Instance.SwordWeapon.currentUpgradeLevel, maxLevelText, WeaponManager.Instance.SwordWeapon.upgradeLevelMax);
                break;
            case WeaponTypeText.SHIELD_TEXT:
                DisplayLevels(currentLevelText, WeaponManager.Instance.ShieldWeapon.currentUpgradeLevel, maxLevelText, WeaponManager.Instance.ShieldWeapon.upgradeLevelMax);
                break;
            case WeaponTypeText.FIRE_ORB_TEXT:
                DisplayLevels(currentLevelText, WeaponManager.Instance.FireOrbWeapon.currentUpgradeLevel, maxLevelText, WeaponManager.Instance.FireOrbWeapon.upgradeLevelMax);
                break;
            case WeaponTypeText.DEATH_SPIRITS_TEXT:
                DisplayLevels(currentLevelText, WeaponManager.Instance.DeathSpiritsWeapon.currentUpgradeLevel, maxLevelText, WeaponManager.Instance.DeathSpiritsWeapon.upgradeLevelMax);
                break;
        }
    }

    public void DisplayLevels(Text currentLevelText, int currentLevel, Text maxLevelText, int maxLevel)
    {
        currentLevelText.text = "Current Level: " + currentLevel;
        maxLevelText.text = "Max Level: " + maxLevel;
    }
}
