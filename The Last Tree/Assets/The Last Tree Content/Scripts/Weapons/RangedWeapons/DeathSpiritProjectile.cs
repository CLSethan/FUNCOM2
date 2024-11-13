using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpiritProjectile : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Collider2D hitCollider;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        hitCollider.enabled = false;
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
    }

    private void DisableHitCollider()
    {
        hitCollider.enabled = false;
    }
}
