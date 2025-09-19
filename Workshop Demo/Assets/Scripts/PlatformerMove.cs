/*
Hey there! little note from Michael here:
This code only turns input into code, but its not particularly designed to 'play well'
So for that, I challenge you to explore! Try applying game design concepts to make the character feel better to control.
Try messing with the physics of the rigidbody. Mass? linear dampening? jump power and gravity?
If you're confident with coding, try adding a fast fall when the play lets go of the jump button
*/

using UnityEngine;
using UnityEngine.InputSystem;


public class PlatformerMove : MonoBehaviour
{
    //FIELDS
    public float moveSpd;
    public float jumpPwr;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpInput;
    private RaycastHit2D groundCheck;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //every frame where we have an input value, change the speed
        rb.linearVelocityX = moveInput * moveSpd;

//Debug is an important collection of functions to know! it helps with debugging.
Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.red);
        //if at any frame we have a jump input and we detect the ground, change the Y velocity
        if(jumpInput && isGrounded())
        {
Debug.Log("check");
            rb.linearVelocityY = jumpPwr;
        }
    }

    //functions named OnXXX are in general working in conjunction with the Input Action System controls and refer to an Action
    private void OnMove(InputValue input)
    {
        moveInput = input.Get<float>();
    }

    private void OnJump(InputValue input)
    {
        jumpInput = input.isPressed;
    }

    //Using a Raycast to check if the ground is close enough to be considered "on the ground"
    private bool isGrounded()
    {
        groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return groundCheck;
    }
}
