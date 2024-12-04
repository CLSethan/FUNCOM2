using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
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
	[SerializeField] PlayerHealth _playerHealth;
	[SerializeField] private GameObject NotificationBoard;
	[SerializeField] private GameObject HowToPlayMenu;
	[SerializeField] private GameObject OptionsMenu;
	[SerializeField] private GameObject PauseMenu;
	public Player player;
	public Transform playerSpawnPoint;

	public PlayerExperience PlayerExperience { get { return _playerExperience; } set { _playerExperience = value; } }

	public Player Player { get { return _player; } set { _player = value; } }

	public PlayerController PlayerController { get { return _playerController; } set { _playerController = value; } }

	public PlayerHealth PlayerHealth { get { return _playerHealth; } set { _playerHealth = value; } }

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

	public void PlayerDied()
	{
		Rigidbody2D _playerMass = player.GetComponent<Rigidbody2D>();
		SpriteRenderer _playerSprite = player.GetComponent<SpriteRenderer>();
		CapsuleCollider2D _playerCollider = player.GetComponent<CapsuleCollider2D>();

		SFXManager.instance.PlaySFXPitched(2);
		_playerMass.mass = 1000;
		_playerSprite.color = new Color(255f, 255f, 255f, 0f);
		_playerController.enabled = false;
		_playerCollider.enabled = false;
		_weaponManager.gameObject.SetActive(false);
		EnemySpawner.playerDead();	
        StartCoroutine(respawn());
	}

	public IEnumerator respawn()
    {
		Rigidbody2D _playerMass = player.GetComponent<Rigidbody2D>();
		SpriteRenderer _playerSprite = player.GetComponent<SpriteRenderer>();
		CapsuleCollider2D _playerCollider = player.GetComponent<CapsuleCollider2D>();
		
        yield return new WaitForSeconds(3f);

        player.transform.localPosition = new UnityEngine.Vector2(0, 0);
        _playerSprite.color = new Color(255f, 255f, 255f, 255f);
        _weaponManager.gameObject.SetActive(true);
        _playerMass.mass = 1;
        _playerHealth.currentHealth = _playerHealth.maxHealth;
		_playerHealth.healthSlider.value = _playerHealth.currentHealth;
        _playerController.enabled = true;
		_playerCollider.enabled = true;
        EnemySpawner.playerAlive();
    }


}
