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

    [SerializeField] private Camera _playerCamera;

    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private WeaponManager _weaponManager;

    private SpriteRenderer _spriteRenderer;

    private float flashDuration = 0.2f;
    
    private Color flashColor = Color.red;

    private Color originalColor;

    public float shakeDuration = 0.5f; 

    public float shakeAmount = 0.7f; 

    public float decreaseFactor = 1.0f;


    private UnityEngine.Vector3 playerCameraOriginalPosition;

    private void Awake()
    {
        GameManager.Instance.PlayerHealth = this;
        instance = this;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = _spriteRenderer.color;
    }

    void Start()
    {
        playerCameraOriginalPosition = _playerCamera.transform.localPosition; 
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
            GameManager.Instance.PlayerDied();
        }
        healthSlider.value = currentHealth;
        StartCoroutine(Flash());
    }

    public void ResetPlayerHealth()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
    }

    public IEnumerator Flash()
    {

    float elapsed = 0f;

    while (elapsed < shakeDuration)
    {
        float x = Random.Range(-1f, 1f) * shakeAmount;
        float y = Random.Range(-1f, 1f) * shakeAmount;
        _playerCamera.transform.localPosition = new UnityEngine.Vector3(playerCameraOriginalPosition.x + x, playerCameraOriginalPosition.y + y, playerCameraOriginalPosition.z);
        elapsed += Time.deltaTime * decreaseFactor;
        yield return null;
    }

    _playerCamera.transform.localPosition = playerCameraOriginalPosition; // Reset position after shaking

    _spriteRenderer.color = flashColor;
    yield return new WaitForSeconds(flashDuration);
    _spriteRenderer.color = originalColor;
    }

}
