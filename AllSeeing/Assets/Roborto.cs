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
    public float RampJumps = 0;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform RampCheckL;
    [SerializeField] private Transform RampCheckR;
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
            switch (RampJumps) 
            {
                case 0:
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                    EdgeCompensation = FloorCheckRadius;
                    break;
                case 1:
                    rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y * 0.5f);
                    EdgeCompensation = FloorCheckRadius;
                    break;
                case 2:
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                    EdgeCompensation = FloorCheckRadius;
                    break;
                case 3:
                    rb.velocity = new Vector2(rb.velocity.x * 0.98f, rb.velocity.y * 1.4f);
                    EdgeCompensation = FloorCheckRadius;
                    RampJumps = 0;
                    rampJump = false;
                    break;
                default:
                    rb.velocity = new Vector2(rb.velocity.x * 0.98f, -2f);
                    rampJump = false;
                    EdgeCompensation = FloorCheckRadius;
                    break;

            }
            //rb.velocity = new Vector2(rb.velocity.x, -2f);
            //EdgeCompensation = FloorCheckRadius;
            //RampJumps = 0;

        }
        else if (AreYaRamped() && rb.velocity.y >= 0f)
        {
            jumps = 1;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            EdgeCompensation = FloorCheckRadius;
            rampJump = true;

        }
        else
        {
            EdgeCompensation -= Time.deltaTime;
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

                switch (RampJumps) 
            {
                case 0:
                    rb.velocity = new Vector2(rb.velocity.x * 0.8f, jumpPower * 0.5f);
                    break;
                case 1:
                    rb.velocity = new Vector2(rb.velocity.x * 0.98f, jumpPower * 0.8f);
                    break;
                case 2:
                    rb.velocity = new Vector2(rb.velocity.x * 0.96f, jumpPower * 0.8f);
                    break;
                case 3:
                    rb.velocity = new Vector2(rb.velocity.x , jumpPower * 1.6f);
                    RampJumps = 0;
                    rampJump = false;
                    break;
                default:
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                    rampJump = false;
                    break;

            }
                jumps = 0;

            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                RampJumps = 0;
            }
            NextJumpCompensation = 0f;
        }
        else if (Input.GetButtonDown("Jump") && jumps > 0 && RampJumps == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, djPower);
            jumps -= 1;
            RampJumps = 0;

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
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, rb.velocity.x + (moveSpeed * airHorizMult / (RampJumps + 5)) * Direction, horizAccel * Time.deltaTime), rb.velocity.y);
                // Debug.Log("Direction = " + Direction + "Changing X Velocity is = " + rb.velocity.x + " MaxChange = " + (horizDeccel * Time.deltaTime));
            }
            else if (AreYaGrounded() == true)
            {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveSpeed * Direction, horizAccel * Time.deltaTime), rb.velocity.y);

            }
            else if (AreYaRamped() == true)
            {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveSpeed * 0.5f * Direction, horizAccel * Time.deltaTime), rb.velocity.y);
            }
            else
            {

                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveSpeed * airHorizMult * Direction, horizAccel * Time.deltaTime), rb.velocity.y);
            }
        }


    }

    private bool AreYaRamped()
    {

        return (Physics2D.OverlapCircle(RampCheckL.position, FloorCheckRadius, ramp) || Physics2D.OverlapCircle(RampCheckR.position, FloorCheckRadius, ramp));
    }


    private bool AreYaGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, FloorCheckRadius, ground);
    }

}
