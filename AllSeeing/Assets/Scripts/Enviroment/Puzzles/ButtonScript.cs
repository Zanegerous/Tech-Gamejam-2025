using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool ButtonPressed { get; private set; }
    public ButtonState CurrentButtonState { get; private set; }


    // Untested as I dont have a player to test it with yet
    void OnTriggerStay(Collider other)
    {
        if (CurrentButtonState == ButtonState.UNPRESSED)
        {
            SetButtonState(ButtonState.PRESSING);
        }
    }

    void OnTriggerEx2D(Collider2D collision)
    {
        SetButtonState(ButtonState.UNPRESSED);
    }


    void SetButtonState(ButtonState state)
    {
        CurrentButtonState = state;
        switch (state)
        {
            case ButtonState.UNPRESSED:
                // Idle
                break;
            case ButtonState.PRESSING:
                // run animation and set to pressed
                break;
            case ButtonState.PRESSED:
                // It is pressed, maybe locked in place?
                break;
        }
    }
}
