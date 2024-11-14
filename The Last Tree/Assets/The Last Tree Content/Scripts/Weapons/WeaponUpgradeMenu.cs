using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> currentUpgradeBoardGameObjectList;
    [SerializeField] private List<GameObject> maxUpgradeBoardGameObjectList;
    [SerializeField] private List<Transform> boardAttachpointList;
    [SerializeField] private List<GameObject> upgradeInstanceList;
    private int upgradeRandomNumber;
    [SerializeField] private int maxUpgradeBoardSlots = 3;

    private void Awake()
    {
        GameManager.Instance.WeaponUpgradeMenu = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRandomWeaponUpgrades()
    {
        //if != null or smtn for the weaponUpgrade to not appear again or smtn

        currentUpgradeBoardGameObjectList = new List<GameObject>(maxUpgradeBoardGameObjectList);

        for (int i = 0; i < maxUpgradeBoardSlots; i++)
        {
            upgradeRandomNumber = Random.Range(0, currentUpgradeBoardGameObjectList.Count);

            Debug.Log($"upgradeRandomNumber: {upgradeRandomNumber}");

            upgradeInstanceList[i] = Instantiate(currentUpgradeBoardGameObjectList[upgradeRandomNumber], boardAttachpointList[i].transform.position, boardAttachpointList[i].rotation);
            upgradeInstanceList[i].transform.SetParent(this.transform);

            currentUpgradeBoardGameObjectList.RemoveAt(upgradeRandomNumber);
        }
    }

    public void DestroyWeaponUpgradeInstances()
    {
        foreach (GameObject upgradeInstance in upgradeInstanceList)
        {
            if (upgradeInstance != null)
            {
                Destroy(upgradeInstance);
            }
        }
    }
}
