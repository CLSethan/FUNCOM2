using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MenuType
{
	MainMenu = 0,
	/*CharacterSelect = 1,*/
	InGameMenu = 1,
    GameOverMenu = 2,
}


public class MenuManager : Singleton<MenuManager>
{
	[SerializeField] GameObject[] menus;
	[SerializeField] public AudioSource buttonClickSound;

	private void Start()
	{
		SwitchMenu((int)MenuType.MainMenu);
	}
	public void SwitchMenu(int index)
	{
		buttonClickSound.Play();

		foreach (GameObject menuObj in menus)
		{
			menuObj.SetActive(false);
		}
		menus[index].SetActive(true);
	}
}
