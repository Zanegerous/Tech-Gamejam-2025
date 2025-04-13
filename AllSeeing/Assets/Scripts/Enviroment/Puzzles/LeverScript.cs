using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public bool LeverOn { get; private set; }
    public LeverState CurrentLeverState { get; private set; }
    public VisualState visualChange;
    private bool InArea = false;
    private Animator animator;
    private string IdleAnimation;
    private string ActiveAnimation;

    public enum LeverColor
    {
        YELLOWLEVER,
        BLUELEVER,
        GREENLEVER,
        REDLEVER,
    }

    public LeverColor animationColor;


    void Start()
    {
        animator = GetComponent<Animator>();
        SetLeverColor(animationColor);
    }


    void Update()
    {
        if (InArea && Input.GetKeyDown(KeyCode.F))
        {
            FlipLeverState();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InArea = false;
        }
    }

    void FlipLeverState()
    {
        if (LeverOn)
        {
            SetLeverState(LeverState.UNFLIPPED);
        }
        else
        {
            SetLeverState(LeverState.FLIPPED);
        }
    }

    void SetLeverState(LeverState state)
    {
        CurrentLeverState = state;
        switch (state)
        {
            case LeverState.UNFLIPPED:
                // Undo thing
                PlayAnimation(IdleAnimation, 0, .25f);
                LeverOn = false;
                break;
            case LeverState.FLIPPED:
                PlayAnimation(ActiveAnimation, 0, .25f);
                // Do thing
                LeverOn = true;
                break;
        }
    }

    void PlayAnimation(string animationName, int frame, float speed)
    {

        animator.speed = speed;
        animator.Play(animationName, 0, frame);

    }

    // Tells it what animation color to use
    void SetLeverColor(LeverColor state)
    {
        switch (state)
        {
            case LeverColor.BLUELEVER:
                IdleAnimation = "BlueLeverIdle";
                ActiveAnimation = "BlueLever";
                PlayAnimation(IdleAnimation, 0, .25f);
                break;
            case LeverColor.GREENLEVER:
                IdleAnimation = "GreenLeverIdle";
                ActiveAnimation = "GreenLever";
                PlayAnimation(IdleAnimation, 0, .25f);
                break;
            case LeverColor.REDLEVER:
                IdleAnimation = "RedLeverIdle";
                ActiveAnimation = "RedLever";
                PlayAnimation(IdleAnimation, 0, .25f);
                break;
            case LeverColor.YELLOWLEVER:
                IdleAnimation = "YellowLeverIdle";
                ActiveAnimation = "YellowLever";
                PlayAnimation(IdleAnimation, 0, .25f);
                break;
        }
    }
}
