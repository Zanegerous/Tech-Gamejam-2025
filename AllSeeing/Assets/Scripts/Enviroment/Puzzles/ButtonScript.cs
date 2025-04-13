using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool ButtonPressed { get; private set; }
    public ButtonState CurrentButtonState { get; private set; }
    private float InArea = 0f;
    private Animator animator;
    private string currentAnimation;
    private bool previousCheck;

    void Start()
    {
        animator = GetComponent<Animator>();
        previousCheck = ButtonPressed;
    }

    void Update()
    {
    }

    void CheckAreaAmmount()
    {
        if (InArea >= 1)
        {
            SetButtonState(ButtonState.PRESSED);
            ButtonPressed = true;
        }
        else
        {
            SetButtonState(ButtonState.UNPRESSED);
            ButtonPressed = false;
        }
    }

    // Untested as I dont have a player to test it with yet
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered by: " + other.name);
        InArea += 1;
        CheckAreaAmmount();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Left by: " + other.name);
        InArea -= 1;
        CheckAreaAmmount();
    }

    void ChangeAnimation(string animation)
    {
        Debug.Log("Starting Animation: " + animation);
        animator.Play(animation);
    }

    void SetButtonState(ButtonState state)
    {
        CurrentButtonState = state;
        switch (state)
        {
            case ButtonState.UNPRESSED:
                // Idle
                ChangeAnimation("ButtonIdle");
                break;
            case ButtonState.PRESSING:
                // run animation and set to pressed
                break;
            case ButtonState.PRESSED:
                ChangeAnimation("ButtonPress");
                break;
        }
    }
}
