using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealth : MonoBehaviour
{
    public static TreeHealth instance;

    public float currentHealth, maxHealth;

    private SpriteRenderer spriteRendererUpper;
    private SpriteRenderer spriteRendererLower;
    private Color originalColor;

    public float flashDuration = 0.1f; // Duration of the flash
    public Color flashColor = Color.red; // Color to flash
    public GameObject upperTreeSprite;
    public GameObject lowerTreeSprite;
    [SerializeField] private GameObject gameOverScreen;

    public Slider treeHpSlider;

    private void Awake()
    {
        instance = this;

         // Get the SpriteRenderer component attached to the upper and lower sprites.
        spriteRendererUpper = upperTreeSprite.GetComponent<SpriteRenderer>();
        spriteRendererLower = lowerTreeSprite.GetComponent<SpriteRenderer>();
        // Store the original color of the sprite
        originalColor = spriteRendererUpper.color;
        originalColor = spriteRendererLower.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        treeHpSlider.maxValue = maxHealth;
        treeHpSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float enemyDamage)
    {
        currentHealth -= enemyDamage;
        StartCoroutine(Flash());

        if (currentHealth <= 0)
        {
            Debug.Log("tree has died");
            GameManager.Instance.MenuManager.SwitchMenu(2);
        }

        treeHpSlider.value = currentHealth;
    }

    private IEnumerator Flash()
    {
        // Change the color to the flash color
        spriteRendererUpper.color = flashColor;
        spriteRendererLower.color = flashColor;
        
        // Wait for the specified duration
        yield return new WaitForSeconds(flashDuration);
        
        // Revert back to the original color
        spriteRendererUpper.color = originalColor;
        spriteRendererLower.color = originalColor;
    }
}
