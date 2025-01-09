using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    //Get references to our Rigidbody, SpriteRenderer
    //and set up some tweakable variables in the inspector for flexibility and ease of use.
    
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Header("Variables")]
    [SerializeField] private float movementInputX;
    [SerializeField] float speedMultiplier =  10f;
    [SerializeField] float jumpHeight =  10f;
    [SerializeField] bool isGrounded;
    
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    
    //We want to update the player position on every physics tick rather than on every frame
    //...because a Rigidbody is involved
    private void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(movementInputX * speedMultiplier, rigidBody.linearVelocity.y);
        FlipSprite();
    }
    
    //Does as it says on the tin...
    private void FlipSprite()
    {
        if (movementInputX <= -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInputX >= 0.01f)
        {
            spriteRenderer.flipX = false;
        }
    }
    
    //new Input System moment...
    //(yeah, no more if(Input.GetButton) yada yada yada...)
    void OnMove(InputValue value)
    {
        movementInputX = value.Get<float>();
        
    }
    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpHeight);
            isGrounded= !isGrounded;
        }

    }

    void OnAttack(InputValue value)
    {
        throw new NotImplementedException();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
