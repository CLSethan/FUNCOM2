using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public float currentHealth, maxHealth;

    public Slider healthSlider;

    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private WeaponManager _weaponManager;

    private SpriteRenderer _spriteRenderer;

    private PlayerController _playerController;

    private Rigidbody2D _playerRb;

    Color initialSprite;

    private void Awake()
    {
        instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerRb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
        initialSprite = _spriteRenderer.color;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();

    }

    public void addHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Healed Player");
        healthSlider.value = currentHealth;
    }

    public void takeDamage(float enemyDamage)
    {
        currentHealth -= enemyDamage;
        if (currentHealth <= 0)
        {
            playerDied();
        }
        healthSlider.value = currentHealth;
    }

    public void playerDied()
    {
        _spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        _weaponManager.gameObject.SetActive(false);
        _playerRb.mass = 1000;
        SFXManager.instance.PlaySFXPitched(2);

        _playerController.enabled = false;
        enemySpawner.playerDead();
        StartCoroutine(respawn());
    }

    public IEnumerator respawn()
    {
        yield return new WaitForSeconds(10f);
        transform.localPosition = new UnityEngine.Vector2(0, 0);
        _spriteRenderer.color = initialSprite;
        _weaponManager.gameObject.SetActive(true);
        _playerRb.mass = 1;
        currentHealth = maxHealth;
        
        _playerController.enabled = true;
        enemySpawner.playerAlive();
    }
}
