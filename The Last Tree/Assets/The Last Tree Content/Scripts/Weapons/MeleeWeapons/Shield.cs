using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Shield : MeleeWeapon
{
    [SerializeField] private float reloadTime;
    [SerializeField] private bool canTrigger;
    [SerializeField] private GameObject shieldCollider;

                    private float currentScale;
                    public bool isEnlarged;


    private void Awake()
    {
        WeaponManager.Instance.ShieldWeapon = this;
        currentUpgradeLevel = 0;
        upgradeLevelMax = 6;
        currentAttackRate = 0.8f;
        maxAttackRate = 1.2f;
        reloadTime = 0.5f;
    }

    protected override IEnumerator ReloadTime(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        canTrigger = true;
    }

protected override void TriggerAttack()
{
    if (canTrigger)
    {
        StartCoroutine(PerformAttack());
    }
}

private IEnumerator PerformAttack()
{
    // Projectile _projectile = shieldCollider.GetComponent<Projectile>();
    canTrigger = false;

    //When the shield starts growing in size, it's damage is set to 20.
    isEnlarged = true;
    // _projectile.damage = 20;
    shieldCollider.SetActive(true);

    Vector3 initialScale = transform.localScale;
    Vector3 targetScale = new Vector3(currentAttackRate, currentAttackRate, currentAttackRate);
    
    // Scale up
    float scaleDuration = 0.2f;
    float elapsedTime = 0f;
    
    while (elapsedTime < scaleDuration)
    {
        transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDuration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }
    
    transform.localScale = targetScale;
    
    yield return new WaitForSeconds(0.01f); 

    // Scale down
    elapsedTime = 0f;
    
    while (elapsedTime < scaleDuration)
    {
        transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsedTime / scaleDuration);
        elapsedTime += Time.deltaTime;
        yield return null; 
    }

    transform.localScale = initialScale;

    // When the shield is in it's idle state, it deals no damage.
    isEnlarged = false;
    // _projectile.damage = 0;
    shieldCollider.SetActive(false);

    StartCoroutine(ReloadTime(reloadTime));
}

    protected override void Update()
    {
        if (canTrigger)
        {
            TriggerAttack();
        }
    }

    public override void Upgrade()
    {
        currentAttackRate = Mathf.Min(currentAttackRate + 0.1f, maxAttackRate);
        currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
    }
    
// private void OnTriggerEnter2D(Collider2D other)
//     {

//         Debug.Log(other.gameObject.name);
//         // Check if the object we collided with has a Rigidbody2D
//         if (other.gameObject.name == "EnemySkeleton" || other.gameObject.name == "EnemyGoblin" || other.gameObject.name == "EnemyFlyingEye" )
//         {
//             Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
//             if (enemyRb != null)
//             {
//                 // Calculate the direction of the knockback
//                 Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

//                 // Apply the knockback force
//                 enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
//             }
//         }
//     }


}