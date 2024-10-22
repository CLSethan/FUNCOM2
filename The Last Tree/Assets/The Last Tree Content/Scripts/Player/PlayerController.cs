using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement
    [SerializeField]
    private float moveSpeed;
    private PlayerControls playerControls;
    private Vector2 movement;
    [SerializeField]
    private Transform firePoint;

    private Rigidbody2D rb;
    private Animator anim;


    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Enable player input system
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        RotateFirepoint();
        FlipCharacterSprite();
    }

    private void PlayerInput()
    {
        // Read player input action on movement action map
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        Vector2 currentPosition = rb.position;
        Vector2 moveToPosition = currentPosition + movement * (moveSpeed * Time.fixedDeltaTime);

        rb.MovePosition(moveToPosition);
    }

    private void RotateFirepoint()
    {
        // If there's movement input, update the rotation
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void FlipCharacterSprite()
    {
        if (rb.velocity.x > movement.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (rb.velocity.x < movement.x)
        {
            transform.localScale = Vector3.one;
        }
    }
}