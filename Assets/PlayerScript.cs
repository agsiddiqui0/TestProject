using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private bool isGrounded;
    private int score = 0;
    private int jumpScore = 0;
    private SpriteRenderer sr;

    [SerializeField] int speed = 0;
    [SerializeField] int jumpHeight = 3;
    [SerializeField] int dashSpeed = 100;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            score++;
            Debug.Log("My score is: " + score);
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


        animator.SetBool("Walk_Right", !Mathf.Approximately(movementVector.x, 0));
        if(!Mathf.Approximately(movementVector.x, 0))
        {
            sr.flipX = movementVector.x < 0;
        }
    }

    void OnJump(InputValue value)
    {
        
        if (jumpScore <= 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpScore++;
            Debug.Log("Jumpscore is " + jumpScore);
        }
        else if(isGrounded)
            jumpScore = 0;
    }

    void OnRightDash()
    {
        Debug.Log("In Dash");
        rb.velocity = new Vector2(movementVector.x * dashSpeed, rb.velocity.y);
        rb.AddForce(new Vector2(600, 10));
    }
    void OnLeftDash()
    {
        Debug.Log("In Dash");
        rb.velocity = new Vector2(movementVector.x * dashSpeed, rb.velocity.y);
        rb.AddForce(new Vector2(-600, 10));
    }
}
