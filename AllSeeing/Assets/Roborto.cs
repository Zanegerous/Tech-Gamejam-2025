using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Roborto : MonoBehaviour
{
    #region Defining Stuff
    public float apexSlowdown;
    private float horizontal;
    public float FloorCheckRadius;
    public float jumpPower;
    public float djPower;
    //private bool facingright = true;
    public float jumps;
    private float EdgeCompensation;
    public float NextJumpCompensation;
    public bool rampJump = false;
    


    private float moveSpeed = 8f;
    private float horizAccel = 50f;
    private float horizDeccel = 100f;
    public float airHorizMult;
    public float RampJumps;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask ramp;

    
    #endregion

    //public Transform spawnPt;



    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (AreYaGrounded() && rb.velocity.y <= 0f)
        {
            jumps = 1;
            rb.velocity = new Vector2(rb.velocity.x, -2f);
            EdgeCompensation = FloorCheckRadius;
            //RampJumps = 0;

        }
        else if (AreYaRamped() && rb.velocity.y >= 0f)
        {
            jumps = 1;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            EdgeCompensation = FloorCheckRadius;
            rampJump = true;

        }
        else {
            EdgeCompensation-= Time.deltaTime;
        }
        

        if (Input.GetButtonDown("Jump")) 
        {
            NextJumpCompensation = 0.3f;
        }
        else
        {
            NextJumpCompensation -= Time.deltaTime;
        }


        #region Regular Ground
        if (EdgeCompensation > 0f && jumps > 0 && NextJumpCompensation > 0f)
        {
            if (rampJump == true) 
            {
                Debug.Log("Doin' A Ramp Jump. " + AreYaGrounded());
                
                RampJumps += 1;

                if (RampJumps == 3) 
                {
                    rb.velocity = new Vector2(rb.velocity.x * 0.8f, jumpPower * 1.4f);
                    rampJump = false;
                    RampJumps = 0;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                }
                jumps = 0;

            }
            else {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            NextJumpCompensation = 0f;
        }
        else if (Input.GetButtonDown("Jump") && jumps > 0 && rampJump == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, djPower);
            jumps -= 1;
            
        }
        #endregion

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * apexSlowdown);
            EdgeCompensation = 0f;
        }
        
    }


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
                rampJump = false;
            }
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, horizDeccel * Time.deltaTime), rb.velocity.y);
            
        }
        else
        {
            if (rampJump == true)
            {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, rb.velocity.x + (moveSpeed / (RampJumps + 1)) * Direction, horizAccel * Time.deltaTime), rb.velocity.y);
                Debug.Log("Direction = " + Direction + "Changing X Velocity is = " + rb.velocity.x + " MaxChange = " + (horizDeccel * Time.deltaTime));
            }
            else if (AreYaGrounded() == true)  
            {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveSpeed * Direction, horizAccel * Time.deltaTime), rb.velocity.y);
                
            }
            else
            {

                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveSpeed * airHorizMult * Direction, horizAccel * Time.deltaTime), rb.velocity.y);
            }
        }
            
        
    }

    private bool AreYaRamped()
    {

        return Physics2D.OverlapCircle(groundCheck.position, FloorCheckRadius, ramp);
    }


    private bool AreYaGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, FloorCheckRadius, ground);
    }

}
