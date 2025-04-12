using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roborto : MonoBehaviour
{
    private float moveSpeed = 8f;
    private float horizontal;
    private float jump = 16f;
    private bool facingright = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;


    public float gravity;
    //public Transform spawnPt;



    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && AreYaGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Turn();
    }

    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private bool AreYaGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    private void Turn() 
    {
        if (facingright && horizontal < 0f || !facingright && horizontal > 0f) {
            facingright = !facingright;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }

}
