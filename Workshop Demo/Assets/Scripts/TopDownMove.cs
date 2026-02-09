using UnityEngine;
using UnityEngine.InputSystem; //<-- If you're starting a new script, this doesn't come included. 
                               // Remember this if you want to use the Input Action System!


public class TopDownMove : MonoBehaviour
{
    /* FIELDS */
    // When fields are set to public, they are editable directly in the inspector!
    // No need to open up the code every time, change a value, then recompile.
    // Just focus on the logic first, and let the number balancing be a second step!
    // Older versions of Unity used [SerializeField] to make things available in the inspector, 
    // but now we can just make them public.
    public float moveSpd;

    // Private fields, on the other hand, are not visible in the inspector.
    // This is useful for values that we need to control or keep track of, but we don't need/want 
    // to touch them in inspector.
    private Rigidbody2D rb; // rb stands for Rigidbody. This is that physics component attached
                            // to the object. We'll use it to move the object around.
    
    private Vector2 moveInput; // This Vector2 is used to store input from the Input Action System.


    /* UNITY FUNCTIONS */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // in order to work with the physics of the object, we need to get the Rigidbody2D 
        // component that is also attached to the same GameObject as this script.
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // Fixed Update is called on a fixed time interval, and is used for physics updates.
    // This helps keeps physics updates consistent across different frame rates.
    //
    // So, I changed Update() to FixedUpdate() because we are working with physics (the Rigidbody).
    // Technically, we don't need physics for the top down genre of movement.
    // But it's good practice to use physics for movement, and it will make it easier to add more 
    // complex mechanics later on.
    void FixedUpdate()
    {
        //every physics update where we have an input value, change the speed
        rb.linearVelocity = moveInput * moveSpd;
    }


    /* INPUT ACTION SYSTEM FUNCTIONS */
    // In general, functions named On[name of Action] are working in conjunction with the 
    // Input Action System's controls and refer to whatever you named an Action.
    // Also, these are never called in the code, but are called automatically 
    // by the Input Action System when we have an input for that Action.
    private void OnMove(InputValue input) 
    {
        // The parameter is basically always the same for functions using the Input Action System.
        // So we get a Vector2 because that's what we set 
        // the Action's type to in the Input Action Asset.
        moveInput = input.Get<Vector2>();
    }
}
