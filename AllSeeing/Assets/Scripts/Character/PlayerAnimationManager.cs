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
    private bool walking;
    // Start is called before the first frame update

    private void OnDestroy()
    {
        CurrentVisualManager.Instance.OnVisualStateChanged -= SetColor;
    }

    void Start()
    {
        CurrentVisualManager.Instance.OnVisualStateChanged += SetColor;
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
            TryPlayWalkSound();
        }
        else if (movementDirection > .2f)
        {
            SpriteTexture.flipX = false;
            SetAnimationState(MovementState.Walking);
            TryPlayWalkSound();
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


    void SetColor(VisualState state)
    {
        switch (state)
        {
            case VisualState.COLORLESS_VIEW:
                //SpriteTexture.color = Color.clear;
                break;
            case VisualState.RED_VIEW:
                //SpriteTexture.color = Color.red;
                break;
            case VisualState.GREEN_VIEW:
                //SpriteTexture.color = Color.green;
                break;
            case VisualState.BLUE_VIEW:
                //SpriteTexture.color = Color.blue;
                break;
        }
    }

    void TryPlayWalkSound()
    {
        if (!walking)
        {
            StartCoroutine(PlayWalkSound());
        }
    }

    IEnumerator PlayWalkSound()
    {
        walking = true;

        AudioClip clip = SoundManager.Instance.GetClip(SoundType.WALK);
        if (clip != null)
        {
            SoundManager.Instance.Play(SoundType.WALK);
            yield return new WaitForSeconds(clip.length);
        }

        walking = false;
    }

    void SetAnimationState(MovementState state)
    {
        AnimationState = state;
        switch (state)
        {
            case MovementState.Idle:
                ChangeAnimation("Idle2");

                break;
            case MovementState.Walking:
                ChangeAnimation("WalkingAnimation2");

                break;
            case MovementState.Jumping:
                ChangeAnimation("JumpAnimation2");
                SoundManager.Instance.Play(SoundType.JUMP);
                break;
        }
    }
}
