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
                CurrentVisualManager.Instance.State = VisualState.COLORLESS_VIEW;
                LeverOn = false;
                break;
            case LeverState.FLIPPED:
                CurrentVisualManager.Instance.State = visualChange;
                // Do thing
                LeverOn = true;
                break;
        }
    }
}
