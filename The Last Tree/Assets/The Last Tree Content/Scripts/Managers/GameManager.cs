using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] WeaponManager _weaponManager;
	[SerializeField] MenuManager _menuManager;
	[SerializeField] WeaponUpgradeMenu _weaponUpgradeMenu;
	[SerializeField] private GameObject NotificationBoard;
	[SerializeField] private GameObject HowToPlayMenu;
	[SerializeField] private GameObject OptionsMenu;
	[SerializeField] private GameObject PauseMenu;

	public WeaponManager WeaponManager { get { return _weaponManager; } set { _weaponManager = value; } }

	public MenuManager MenuManager { get { return _menuManager; } set { _menuManager = value; } }

	public WeaponUpgradeMenu WeaponUpgradeMenu { get { return _weaponUpgradeMenu; } set { _weaponUpgradeMenu = value; } }

	/*    public void StartGame()
		{
			ResetGameInstances();
			Time.timeScale = 1;
		}

		public void DefaultCharacterPick()
		{
			Player.characterType = CharacterType.DEFAULT;

			ResetGameInstances();

			Time.timeScale = 1;
		}

		public void SpeedCharacterPick()
		{
			Player.characterType = CharacterType.SPEED;

			ResetGameInstances();

			Time.timeScale = 1;
		}

		public void TankCharacterPick()
		{
			Player.characterType = CharacterType.TANK;

			ResetGameInstances();

			Time.timeScale = 1;
		}

		public void RestartGame()
		{
			ResetGameInstances();
			Time.timeScale = 1;
		}

		public void ResetGameInstances()
		{
			SpawnerController.Instance.Reset();
			Player.Reset();
			ScoreManager.ResetScore();
		}



		public void GameOver()
		{
			Time.timeScale = 0;
			MenuMgr.Instance.SwitchMenu((int)MenuType.GameOverMenu);
			ScoreManager.Instance.UpdateGameOverScoreText();
		}*/

	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MenuManager.GetCurrentMenuType() == MenuType.InGameMenu)
        {
			if (!WeaponManager.isUpgradeMenuActive)
			{
				if (!PauseMenu.activeSelf)
				{
					PauseMenu.SetActive(true);
					Time.timeScale = 0;
				}
				else
				{
					Time.timeScale = 1;
					PauseMenu.SetActive(false);
				}
			}
        }
    }

    public void DestroyNotificationBoard()
	{
		MenuManager.buttonClickSound.Play();

		Destroy(NotificationBoard);
	}

	public void ActivateHowToPlayMenu()
	{
		MenuManager.buttonClickSound.Play();

		HowToPlayMenu.SetActive(true);
	}

	public void DeactivateHowToPlayMenu()
	{
		MenuManager.buttonClickSound.Play();

		HowToPlayMenu.SetActive(false);
	}

	public void ActivateOptionsMenu()
	{
		MenuManager.buttonClickSound.Play();

		OptionsMenu.SetActive(true);
	}

	public void DeactivateOptionsMenu()
	{
		MenuManager.buttonClickSound.Play();

		OptionsMenu.SetActive(false);
	}

	public void ResetGameInstances()
	{
/*		SpawnerController.Instance.Reset();
		Player.Reset();
		ScoreManager.ResetScore();*/
	}

	public void ContinueGame()
    {
		Time.timeScale = 1;
		MenuManager.buttonClickSound.Play();
		PauseMenu.SetActive(false);
	}

	public void QuitGame()
	{
		MenuManager.buttonClickSound.Play();

		Application.Quit();
	}
}
