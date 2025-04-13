using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public MovementState AnimationState { get; private set; }
    private Animator animator;
    private string currentAnimation = "";
    private float movementDirection;
    private SpriteRenderer SpriteTexture;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SpriteTexture = GetComponent<SpriteRenderer>();
        ChangeAnimation("Idle");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDirection();
    }

    void CheckDirection()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");

        if (movementDirection < -.2f)
        {
            SpriteTexture.flipX = true;
            SetAnimationState(MovementState.Walking);
        }
        else if (movementDirection > .2f)
        {
            SpriteTexture.flipX = false;
            SetAnimationState(MovementState.Walking);
        }
        else
        {
            SetAnimationState(MovementState.Idle);
        }
        if (Input.GetButtonDown("Jump"))
        {
            SetAnimationState(MovementState.Jumping);
        }

    }

    void ChangeAnimation(string animation)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, .2f);
        }
    }


    void SetAnimationState(MovementState state)
    {
        AnimationState = state;
        switch (state)
        {
            case MovementState.Idle:
                ChangeAnimation("Idle");
                break;
            case MovementState.Walking:
                ChangeAnimation("WalkingAnimation");
                break;
            case MovementState.Jumping:
                ChangeAnimation("JumpAnimation");
                break;
        }
    }
}
