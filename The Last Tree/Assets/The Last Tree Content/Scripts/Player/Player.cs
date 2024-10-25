using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Animator Animator;
    public RuntimeAnimatorController[] Controllers;
    public GameObject[] PlayerPrefabs;

    [Header("Stats")]
    public float pickupRange;
    public float moveSpeed;
    [SerializeField] private GameObject currentPlayerType;

    void Start()
    {
        SetCurrentPlayerType(PlayerPrefabs[0]);
        Animator.runtimeAnimatorController = Controllers[0];
        ChangeCharacter();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SetCurrentPlayerType(PlayerPrefabs[0]);
            Animator.runtimeAnimatorController = Controllers[0];
            ChangeCharacter();
        }
        
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SetCurrentPlayerType(PlayerPrefabs[1]);
            Animator.runtimeAnimatorController = Controllers[1];
            ChangeCharacter();
        }
       
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SetCurrentPlayerType(PlayerPrefabs[2]);
            Animator.runtimeAnimatorController = Controllers[2];
            ChangeCharacter();
        }

    }

    public void ChangeCharacter()
    {
        for (int i = 0; i < PlayerPrefabs.Length; i++)
        {
            PlayerPrefabs[i].SetActive(false);
            if (currentPlayerType != null && currentPlayerType.GetComponent<GameObject>() == PlayerPrefabs[i].GetComponent<GameObject>())
            {
                PlayerPrefabs[i].SetActive(true);
            }
        }
    }

    public void SetCurrentPlayerType(GameObject player)
    {
        currentPlayerType = player;
    }
}
