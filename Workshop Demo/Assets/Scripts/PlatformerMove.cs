/*
This code only turns input into code, but its not particularly designed to 'play well'
So for that, I challenge you to explore! Try applying game design concepts to make the 
character feel better to control.

Try messing with the physics of the rigidbody. Mass? Linear dampening? Jump power and gravity?

If you're confident with coding, try adding a fast fall when the player lets go of the jump button.
Here's a hint: Check the Trigger Behavior in Interactions under Action Properties for the 
               Jump Action in the Input Action Asset.
*/

using UnityEngine;
using UnityEngine.InputSystem;


public class PlatformerMove : MonoBehaviour
{
    /* FIELDS */
    public float moveSpd;
    public float jumpPwr;
    public LayerMask groundLayer; // A LayerMask is basically a filter for raycasts. 
                                  // It allows us to specify which layers we want to interact with.
                                  // In this case, we only want to interact with the ground layer 
                                  // when we do our ground check raycast.

    private Rigidbody2D rb;
    private float moveInput; // Notice that moveInput is a float, not a Vector2.
                             // Take another look at the Input Action Asset. 
                             // Move is now a 1D Axis, not a 2D Vector.
    private bool jumpInput;
    private RaycastHit2D groundCheck; // This is used to store the result of 
                                      // our raycast, more on what that is later.


    /* UNITY FUNCTIONS */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // ~~Update is called once per frame~~ 
    // We aren't using Update because now we're really working with the physics of the object.
    void FixedUpdate()
    {
        // Since this is a platformer, we only want to change the X velocity with our move input.
        rb.linearVelocityX = moveInput * moveSpd;

// Debug is an important collection of functions to know! it helps with, well, debugging.
// Here, we draw a ray that matches the raycast we use for our ground check (see isGrounded()).
// This is only visible in the Scene view, not the Game view.
Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.red);

        // if at any frame we have a jump input and we detect the ground, change the Y velocity
        if(jumpInput && isGrounded())
        {

// This print statement shows up in the Console.
// We use it to check if we are passing the conditions for the jump.
Debug.Log("passed jump input and ground check conditions");

            // Just like with the horizontal movement, we seperate the Y and X axis.
            rb.linearVelocityY = jumpPwr;
        }
    }


    /* HELPER FUNCTIONS */
    // Theres many ways to check that the player is on the ground, but I'll show you
    // one of the most common ones: Raycasting.
    private bool isGrounded()
    {
        // Imagine an arrow. Thats basically what a raycast is. 
        // It has an origin, a direction, and a length. Sound familiar? 
        // Yup, its basically a vector. And in fact we use a vector to setup the direction.
        // The first parameter is the origin, which we set to the position of the object.
        // The second parameter is the direction, which we set as a vector pointing downwards.
        // The third parameter is the length, which we set to 1.1f - juuust past the object's edge.
        // And finally, the last parameter is the layer mask, which we set to the 
        // groundLayer (that new public field I was talking about above).
        groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        // groundCheck will be a true or false value, depending on if the raycast hits
        // an object with the groundLayer.
        return groundCheck;
    }


    /* INPUT ACTION SYSTEM FUNCTIONS */
    private void OnMove(InputValue input)
    {
        moveInput = input.Get<float>(); // This float is just -1.0 or 1.0.
    }

    private void OnJump(InputValue input)
    {
        jumpInput = input.isPressed; // Since the Jump action type is Button and not Value, 
                                     // we can check if the button is pressed.
    }
}
