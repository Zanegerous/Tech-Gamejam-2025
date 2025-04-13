using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour
{
    // List to cycle through, If visual state is updated than i should update this aswell for more abilities. 
    // Could add logic to it to prevent some abilities till they are "unlocked"
    private List<VisualState> PowerEyeStates = new List<VisualState>
    {
        VisualState.COLORLESS_VIEW,
        VisualState.RED_VIEW,
        VisualState.GREEN_VIEW,
        VisualState.BLUE_VIEW
    };

    private VisualState worldState;


    private SpriteRenderer spriteRenderer;

    private void OnDestroy()
    {
        CurrentVisualManager.Instance.OnVisualStateChanged -= UpdateColor;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        worldState = VisualState.COLORLESS_VIEW;
        CurrentVisualManager.Instance.OnVisualStateChanged += UpdateColor;
    }

    private void Update()
    {
        // Use  this Q and E to switch between abilities
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchLeft();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchRight();
        }
    }

    private void SwitchLeft()
    {
        int index = (int)worldState - 1;
        if (index < 0)
        {
            index = (int)VisualState.BLUE_VIEW;
        }
        worldState = (VisualState)index;
        CurrentVisualManager.Instance.State = worldState;

    }

    private void SwitchRight()
    {
        int index = (int)worldState + 1;
        if (index > (int)VisualState.BLUE_VIEW)
        {
            index = (int)VisualState.COLORLESS_VIEW;
        }
        worldState = (VisualState)index;
        CurrentVisualManager.Instance.State = worldState;
    }
    private void UpdateColor(VisualState state)
    {
        Debug.Log("Switching State to " + state);
        switch (state)
        {
            case VisualState.COLORLESS_VIEW:

                spriteRenderer.color = Color.gray;
                break;
            case VisualState.RED_VIEW:
                spriteRenderer.color = Color.red;
                break;
            case VisualState.GREEN_VIEW:
                spriteRenderer.color = Color.green;
                break;
            case VisualState.BLUE_VIEW:
                spriteRenderer.color = Color.blue;
                break;
        }

    }

    void CycleStates() // Testing
    {
        switch (worldState)
        {
            case VisualState.RED_VIEW:
                worldState = VisualState.GREEN_VIEW;
                CurrentVisualManager.Instance.State = VisualState.GREEN_VIEW;
                //Debug.Log("Switched to Green");
                break;

            case VisualState.GREEN_VIEW:
                worldState = VisualState.BLUE_VIEW;
                CurrentVisualManager.Instance.State = VisualState.BLUE_VIEW;
                //Debug.Log("Switched to Blue");
                break;

            case VisualState.BLUE_VIEW:
                worldState = VisualState.RED_VIEW;
                CurrentVisualManager.Instance.State = VisualState.RED_VIEW;
                //Debug.Log("Switched to Red");
                break;
            case VisualState.COLORLESS_VIEW:

                break;
        }


    }
}
