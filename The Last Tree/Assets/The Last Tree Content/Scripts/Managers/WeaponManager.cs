using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    NONE,
    BOW,
    CROSSBOW,
    ETHEREAL_WARRIOR,
    SWORD,
    SHIELD,
    FIRE_ORB,
    DEATH_SPIRITS
}

public class WeaponManager : Singleton<WeaponManager>
{
    //Copy paste the serializefield and public Bow to connect scripts of weapons to WeaponManager script
    [SerializeField] Crossbow _crossbow;

    public Crossbow CrossbowWeapon { get { return _crossbow; } set { _crossbow = value; } }

    //Copy paste the serializefield and public Bow to connect scripts of weapons to WeaponManager script
    [SerializeField] Bow _bow;

    public Bow BowWeapon { get { return _bow; } set { _bow = value; } }

    //Copy paste the serializefield and public Bow to connect scripts of weapons to WeaponManager script
    [SerializeField] EtherealWarrior _etherealWarrior;

    public EtherealWarrior EtherealWarriorWeapon { get { return _etherealWarrior; } set { _etherealWarrior = value; } }

    //Copy paste the serializefield and public Bow to connect scripts of weapons to WeaponManager script
    [SerializeField] Sword _sword;

    public Sword SwordWeapon { get { return _sword; } set { _sword = value; } }

    //Copy paste the serializefield and public Bow to connect scripts of weapons to WeaponManager script
    [SerializeField] ShieldWeapon _shield;

    public ShieldWeapon ShieldWeapon { get { return _shield; } set { _shield = value; } }

    //Copy paste the serializefield and public Bow to connect scripts of weapons to WeaponManager script
    [SerializeField] FireOrb _fireOrb;

    public FireOrb FireOrbWeapon { get { return _fireOrb; } set { _fireOrb = value; } }

    //Copy paste the serializefield and public Bow to connect scripts of weapons to WeaponManager script
    [SerializeField] DeathSpirits _deathSpirits;

    public DeathSpirits DeathSpiritsWeapon { get { return _deathSpirits; } set { _deathSpirits = value; } }

    [SerializeField] public List<GameObject> weaponList;

    [SerializeField] public WeaponTypes weaponTypes;

    [SerializeField] private GameObject UpgradeMenu;

    [SerializeField] private GameObject FullUpgradeBoard;

    [SerializeField] public bool isUpgradeMenuActive = false;

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

    public void WeaponModifier(WeaponTypes type)
    {
        switch (type)
        {
            case WeaponTypes.NONE:
                break;
            case WeaponTypes.BOW:
                EquipAndUpgradeWeapon(weaponList[0], BowWeapon, type);
                break;
            case WeaponTypes.CROSSBOW:
                EquipAndUpgradeWeapon(weaponList[1], CrossbowWeapon, type);
                break;
            case WeaponTypes.ETHEREAL_WARRIOR:
                EquipAndUpgradeWeapon(weaponList[2], EtherealWarriorWeapon, type);
                break;
            case WeaponTypes.SWORD:
                EquipAndUpgradeWeapon(weaponList[3], SwordWeapon, type);
                break;
            case WeaponTypes.SHIELD:
                EquipAndUpgradeWeapon(weaponList[4], ShieldWeapon, type);
                break;
            case WeaponTypes.FIRE_ORB:
                EquipAndUpgradeWeapon(weaponList[5], FireOrbWeapon, type);
                break;
            case WeaponTypes.DEATH_SPIRITS:
                EquipAndUpgradeWeapon(weaponList[6], DeathSpiritsWeapon, type);
                break;
        }
    }

    public void UpgradeCrossbowButton()
    {
        GameManager.Instance.MenuManager.buttonClickSound.Play();

        if (CrossbowWeapon.currentUpgradeLevel < CrossbowWeapon.upgradeLevelMax)
        {
            WeaponModifier(WeaponTypes.CROSSBOW);
            ResumeGame();

           //UIController.Instance.ShowPlayerUpgradeMenu();

        }
        else
        {
            ActivateFullUpgradeBoard();
        }

    }

    public void UpgradeBowButton()
    {
        GameManager.Instance.MenuManager.buttonClickSound.Play();

        if (BowWeapon.currentUpgradeLevel < BowWeapon.upgradeLevelMax)
        {
            WeaponModifier(WeaponTypes.BOW);
            ResumeGame();
            //UIController.Instance.ShowPlayerUpgradeMenu();

        }
        else
        {
            ActivateFullUpgradeBoard();
        }

    }

    public void UpgradeEtherealWarriorButton()
    {
        GameManager.Instance.MenuManager.buttonClickSound.Play();

        if (EtherealWarriorWeapon.currentUpgradeLevel < EtherealWarriorWeapon.upgradeLevelMax)
        {
            WeaponModifier(WeaponTypes.ETHEREAL_WARRIOR);
            ResumeGame();
            //UIController.Instance.ShowPlayerUpgradeMenu();

        }
        else
        {
            ActivateFullUpgradeBoard();
        }
    }

    public void UpgradeSwordButton()
    {
        GameManager.Instance.MenuManager.buttonClickSound.Play();

        if (SwordWeapon.currentUpgradeLevel < SwordWeapon.upgradeLevelMax)
        {
            WeaponModifier(WeaponTypes.SWORD);
            ResumeGame();
           // UIController.Instance.ShowPlayerUpgradeMenu();

        }
        else
        {
            ActivateFullUpgradeBoard();
        }
    }

    public void UpgradeShieldButton()
    {
        GameManager.Instance.MenuManager.buttonClickSound.Play();

        if (ShieldWeapon.currentUpgradeLevel < ShieldWeapon.upgradeLevelMax)
        {
            WeaponModifier(WeaponTypes.SHIELD);
            ResumeGame();
            //UIController.Instance.ShowPlayerUpgradeMenu();

        }
        else
        {
            ActivateFullUpgradeBoard();
        }
    }

    public void UpgradeFireOrbButton()
    {
        GameManager.Instance.MenuManager.buttonClickSound.Play();

        if (FireOrbWeapon.currentUpgradeLevel < FireOrbWeapon.upgradeLevelMax)
        {
            WeaponModifier(WeaponTypes.FIRE_ORB);
            ResumeGame();
            //UIController.Instance.ShowPlayerUpgradeMenu();

        }
        else
        {
            ActivateFullUpgradeBoard();
        }
    }

    public void UpgradeDeathSpiritsButton()
    {
        GameManager.Instance.MenuManager.buttonClickSound.Play();

        if (DeathSpiritsWeapon.currentUpgradeLevel < DeathSpiritsWeapon.upgradeLevelMax)
        {
            WeaponModifier(WeaponTypes.DEATH_SPIRITS);
            ResumeGame();
            //UIController.Instance.ShowPlayerUpgradeMenu();

        }
        else
        {
            ActivateFullUpgradeBoard();
        }
    }

    public void EquipAndUpgradeWeapon(GameObject weapon, IUpgradeableWeapon weaponClass, WeaponTypes type)
    {
        // Check if the weapon is disabled, if it is, enable the weapon
        if (!weapon.activeSelf)
        {
            weapon.SetActive(true);
            Debug.Log($"{type} Equipped");
        }

        // Check if the weapon can be upgraded further
        if (weaponClass != null && weaponClass.currentUpgradeLevel < weaponClass.upgradeLevelMax)
        {
            weaponClass.Upgrade();
            Debug.Log($"{type} Upgraded");

            // If the weapon has reached its max upgrade, evolve it
            if (weaponClass.currentUpgradeLevel >= weaponClass.upgradeLevelMax)
            {
                weaponClass.Evolve();
                Debug.Log($"{type} Evolved");
            }
        }
/*        else
        {
            Debug.Log($"{type} has reached full upgrade");
        }*/

        // Reset weapon type
        type = WeaponTypes.NONE;
    }

    public void ActivateFullUpgradeBoard()
    {
        if (!FullUpgradeBoard.activeSelf)
        {
            FullUpgradeBoard.SetActive(true);

            StartCoroutine(DeactivateFullUpgradeBoard());

            //then set its active state to false after 2 seconds
        }
    }

    private IEnumerator DeactivateFullUpgradeBoard()
    {
        yield return new WaitForSecondsRealtime(2.5f);

        FullUpgradeBoard.SetActive(false);
    }

    public void ResumeGame()
    {
        UIController.Instance.HideUpgradeMenu();
        //UIController.Instance.ShowPlayerUpgradeMenu();
        GameManager.Instance.WeaponUpgradeMenu.DestroyWeaponUpgradeInstances();
        UIController.Instance.ShowPlayerUpgradeMenu();
    }
}
