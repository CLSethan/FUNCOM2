using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;

    // move to player
    private bool movingToPlayer;
    public float moveSpeed;
    public float timeBetweenChecks = .2f;
    private float checkCounter;

    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        player = PlayerHealth.instance.GetComponent<Player>();
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            PlayerHealth.instance.addHealth(healAmount);
            Destroy(gameObject);
        }
    }

    void MoveToPlayer()
    {
        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        else
        {
            checkCounter -= Time.deltaTime;

            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }
}