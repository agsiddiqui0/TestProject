using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private bool isGrounded;

    [SerializeField] int speed = 0;
    [SerializeField] int jumpHeight = 2;
    [SerializeField] int dashSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("AHHHH");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = movementVector;
        
          rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);

    }

    void OnJump(InputValue value)
    {
        if(isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        
    }

    void OnRightDash()
    {
        Debug.Log("In Dash");
        rb.velocity = new Vector2(movementVector.x * dashSpeed, rb.velocity.y);
        rb.AddForce(new Vector2(600, 0));
    }
    void OnLeftDash()
    {
        Debug.Log("In Dash");
        rb.velocity = new Vector2(movementVector.x * dashSpeed, rb.velocity.y);
        rb.AddForce(new Vector2(-600, 0));
    }
}
