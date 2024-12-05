using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	public static GameManager instance;

	[SerializeField] WeaponManager _weaponManager;
	[SerializeField] MenuManager _menuManager;
	[SerializeField] WeaponUpgradeMenu _weaponUpgradeMenu;
	[SerializeField] EnemySpawner _enemySpawner;
	[SerializeField] PlayerExperience _playerExperience;
	[SerializeField] Player _player;
	[SerializeField] PlayerController _playerController;
	[SerializeField] private GameObject NotificationBoard;
	[SerializeField] private GameObject HowToPlayMenu;
	[SerializeField] private GameObject OptionsMenu;
	[SerializeField] private GameObject PauseMenu;
	public Player player;
	public Transform playerSpawnPoint;
	public PlayerExperience PlayerExperience { get { return _playerExperience; } set { _playerExperience = value; } }

	public Player Player { get { return _player; } set { _player = value; } }

	public PlayerController PlayerController { get { return _playerController; } set { _playerController = value; } }

	public EnemySpawner EnemySpawner { get { return _enemySpawner; } set { _enemySpawner = value; } }

	public WeaponManager WeaponManager { get { return _weaponManager; } set { _weaponManager = value; } }

	public MenuManager MenuManager { get { return _menuManager; } set { _menuManager = value; } }

	public WeaponUpgradeMenu WeaponUpgradeMenu { get { return _weaponUpgradeMenu; } set { _weaponUpgradeMenu = value; } }

    private void Awake()
    {
		instance = this;
    }

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

	public void ResetGameInstances()
	{
		//Time.timeScale = 1;
		// SceneManager.LoadScene("FinalGameScene 1");
		Time.timeScale = 1;
		player.transform.position = playerSpawnPoint.position;
		EnemySpawner.ResetEnemySpawner();
		TreeHealth.instance.ResetTreeHealth();
		Tree.instance.ResetTreeStats();
		PlayerExperience.ResetPlayerExperience();
		WeaponManager.ResetWeaponManager();
		CoinController.instance.ResetCoinController();
		PlayerStatController.instance.ResetPlayerStatController();
		PlayerHealth.instance.ResetPlayerHealth();
		Player.ResetPlayerPickupRange();
		PlayerController.ResetPlayerSpeed();
		PauseMenu.SetActive(false);
	}
}
