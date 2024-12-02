using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MenuType
{
	MainMenu = 0,
	/*CharacterSelect = 1,*/
	InGameMenu = 1,
    GameOverMenu = 2,
	VictoryScreen = 3,
}

public class MenuManager : Singleton<MenuManager>
{
	[SerializeField] public GameObject[] menus;
	[SerializeField] public AudioSource buttonClickSound;

	private MenuType currentMenuType;

	private void Start()
	{
		SwitchMenu((int)MenuType.MainMenu);
	}

	public void SwitchMenu(int index)
	{
		buttonClickSound.Play();

		// Deactivate all menus
		foreach (GameObject menuObj in menus)
		{
			menuObj.SetActive(false);
		}

		// Activate the selected menu and update the current menu type
		menus[index].SetActive(true);
		currentMenuType = (MenuType)index;
	}

	public MenuType GetCurrentMenuType()
	{
		return currentMenuType;
	}
}
