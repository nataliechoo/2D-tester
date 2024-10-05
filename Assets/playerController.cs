using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//additions:
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private bool onGround;
    private int jpHeight = 10;
    private bool jumpRequest = false;
    [SerializeField] int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        //add bounce collision & only on ground when we have a collision
        //rb.AddForce(new Vector2(0,300));

        if (collision.gameObject.CompareTag("Ground")){
            Debug.Log("Hitting Ground");

            //since we are just hitting the ground, we should set onGround to true now
            onGround = true;
        }
    }
    private void OnCollisionExit2D (Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")){
            Debug.Log("Exiting Ground");

            //since we are leaving the ground, we should set onGround to false now
            onGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //we update the coninuous bouncing with each frame
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
        
        // Handle jump input detection, continuously check for if a jump was requested
        if (onGround && Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            jumpRequest = true; // Set jump request flag
            Debug.Log("Jump key pressed");
        }
    }
    private void FixedUpdate() {
        //if we got a jump request, do it in a fixed way so it doesn't get spammed
        if (jumpRequest) {
            Jump();
        }
    }

    //if we get some value from the keypresses, we will get the movement vector to it
    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();

        //get the input value as a 2D vector 
        Debug.Log("movement: " + movementVector);
    }

    void Jump() {
        if (onGround) {
            rb.AddForce(Vector2.up * jpHeight, ForceMode2D.Impulse);
            onGround = false;
            jumpRequest = false;
            Debug.Log("jump exe");
        }
    }
}
