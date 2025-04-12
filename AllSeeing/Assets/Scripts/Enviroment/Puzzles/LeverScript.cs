using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public bool LeverOn { get; private set; }
    public LeverState CurrentLeverState { get; private set; }


    void Update()
    {

    }

    // Check if lever can be flipped
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            FlipLeverState();
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
                LeverOn = false;
                break;
            case LeverState.FLIPPED:
                // Do thing
                LeverOn = true;
                break;
        }
    }
}
