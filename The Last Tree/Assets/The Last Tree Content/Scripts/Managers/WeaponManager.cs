using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    NONE,
    BOW,
}

public class WeaponManager : Singleton<WeaponManager>
{
    //Copy paste the serializefield and public Bow to connect scripts of weapons to WeaponManager script
    [SerializeField] Crossbow _crossbow;

    public Crossbow CrossbowWeapon { get { return _crossbow; } set { _crossbow = value; } }

    [SerializeField] public List<GameObject> weaponList;

    [SerializeField] private WeaponTypes currentWeaponType = WeaponTypes.NONE;

    [SerializeField] WeaponTypes weaponTypes;

    void Awake()
    {
        GameManager.Instance.WeaponManager = this;
    }

    void Start()
    {
        FillWeaponList();
    }

    void Update()
    {
        //Placeholder for a button upgrade/obtain weapon button
/*        if (Input.GetKeyDown(KeyCode.Space))
        {
            weaponTypes = WeaponTypes.BOW;
        }*/
    }

    //fills the weaponList with the child gameObjects under WeaponManager gameObject
    void FillWeaponList()
    {
        weaponList = new List<GameObject>();

        foreach (Transform child in transform)
        {
            GameObject weapon = child.gameObject;
            weaponList.Add(weapon);
        }
    }

    public void EquipAndUpgradeWeapon(WeaponTypes type)
    {
        switch (type)
        {
            case WeaponTypes.NONE:
                break;
            case WeaponTypes.BOW:

                if (!weaponList[0].activeSelf) //checks if bow gameObject is disabled, if it is, enables bow gameObject
                {
                    weaponList[0].SetActive(true);
                    Debug.Log("Bow Equipped");
                }

                if (CrossbowWeapon.currentUpgradeLevel < CrossbowWeapon.upgradeLevelMax)
                {
                    CrossbowWeapon.Upgrade();
                    Debug.Log("Bow Upgraded");

                    if (CrossbowWeapon.currentUpgradeLevel >= CrossbowWeapon.upgradeLevelMax)
                    {
                        CrossbowWeapon.EvolvedBow();
                    }
                }
                else
                {
                    Debug.Log("Bow has reached full upgrade");
                }
                type = WeaponTypes.NONE;
                break;
        }
    }

    public void UpgradeCrossbowButton()
    {
        EquipAndUpgradeWeapon(WeaponTypes.BOW);
    }
}
