using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Roborto : MonoBehaviour
{
    private float horizontal;
    public float jumpPower;
    public float djPower;
    private bool facingright = true;
    public float jumps;

    private float moveSpeed = 8f;
    private float vertAccel = 100f;
    private float horizAccel = 100f;
    private float vertDeccel = 100f;
    private float horizDeccel = 100f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;


    public float gravity;
    public float apexSlowdown;
    //public Transform spawnPt;



    // Update is called once per frame
    void Update()
    {
        if (AreYaGrounded() && rb.velocity.y <= 0f)
        {
            jumps = 1;
            rb.velocity = new Vector2(rb.velocity.x, -1.5f);

        }
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && AreYaGrounded() == true && jumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        else if (Input.GetButtonDown("Jump") && jumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, djPower );
            jumps -= 1;
        }


        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * apexSlowdown);
        }
        

        //Turn();
    }

    // Can double jump if:
    // velocity is less than zero (falling), AND
    // player's DJ usage is set to true ONLY IF
    // player has not jumped yet


    private void FixedUpdate() 
    {

        VelocityCalcX();
    }

    private void VelocityCalcX()
    {
        var Direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (AreYaGrounded() == true)
            {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, horizDeccel * Time.deltaTime), rb.velocity.y);
            }
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, horizDeccel * Time.deltaTime), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveSpeed * Direction, horizAccel * Time.deltaTime), rb.velocity.y);
        }
        
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
