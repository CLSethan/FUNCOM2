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
    [SerializeField] private WeaponTypes currentWeaponType = WeaponTypes.NONE;
    [SerializeField] public List<GameObject> weaponList;
    [SerializeField] WeaponTypes weaponTypes;

    void Start()
    {
        FillWeaponList(); // Populate the weapon list at the start
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            weaponTypes = WeaponTypes.BOW;
        }

        switch (weaponTypes)
        {
            case WeaponTypes.NONE:
                break;
            case WeaponTypes.BOW:

                if (!weaponList[0].activeSelf) // Check if the first weapon is inactive
                {
                    weaponList[0].SetActive(true); // Enable the first weapon
                }

                Debug.Log("Bow Equipped/Upgraded");
                weaponTypes = WeaponTypes.NONE;
                break;
        }
    }

    // This method will fill the list with all child GameObjects under the WeaponManager that represent weapons
    void FillWeaponList()
    {
        weaponList = new List<GameObject>();

        foreach (Transform child in transform)
        {
            GameObject weapon = child.gameObject;
            // Optionally, you can add a check if the child has a specific component, e.g., Weapon script, if you use one
            weaponList.Add(weapon);
        }
    }
}
