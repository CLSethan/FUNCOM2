using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player movement
    [SerializeField]
    private float moveSpeed;
    private PlayerControls playerControls;
    private Vector2 movement;

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
        //enable player input system
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        //FlipCharacterSprite();

    }

    private void PlayerInput()
    {
        //read player input action on movement action map
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);

    }

    private void Move()
    {
        Vector2 currentPosition = rb.position;
        Vector2 moveToPosition = currentPosition + movement * (moveSpeed * Time.fixedDeltaTime);

        rb.MovePosition(moveToPosition);
        FlipCharacterSprite();
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
