using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D theRigidbody;
    public float enemySpeed;
    private Transform target;

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;

    }

    void Update()
    {
       theRigidbody.velocity = (target.position - transform.position).normalized * enemySpeed;
    }
}
