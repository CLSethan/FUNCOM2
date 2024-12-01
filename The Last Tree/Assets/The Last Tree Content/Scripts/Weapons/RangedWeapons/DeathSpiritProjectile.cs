using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpiritProjectile : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Collider2D hitCollider;

    [SerializeField] private GameObject parentObject;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hitCollider.enabled = false;

        //element 1 of MenuManager's menus list (which is InGameMenu)
        parentObject = GameManager.Instance.MenuManager.menus[1];

        if (parentObject != null)
        {
            transform.SetParent(parentObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void HitEnemy()
    {
        hitCollider.enabled = true;
        Invoke("DisableHitCollider", 0.08f);

        StartCoroutine(KillSelf());
    }

    private IEnumerator KillSelf()
    {
        yield return new WaitForSeconds(WeaponManager.Instance.DeathSpiritsWeapon.currentActiveTime);

        anim.SetTrigger("Die");
    }

    private void DisableHitCollider()
    {
        hitCollider.enabled = false;
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        DestroySelf();
    }
}
