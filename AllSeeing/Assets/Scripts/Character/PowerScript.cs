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

    private SpriteRenderer spriteRenderer;

    private void OnDestroy()
    {
        CurrentVisualManager.Instance.OnVisualStateChanged -= UpdateColor;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        int index = PowerEyeStates.IndexOf(CurrentVisualManager.Instance.State);
        if (index == 0)
        {
            CurrentVisualManager.Instance.State = PowerEyeStates[PowerEyeStates.Count - 1]; // loop back to max
        }
        else
        {
            CurrentVisualManager.Instance.State = PowerEyeStates[index - 1];
        }
    }

    private void SwitchRight()
    {
        int index = PowerEyeStates.IndexOf(CurrentVisualManager.Instance.State);
        if (index == (PowerEyeStates.Count - 1))
        {
            CurrentVisualManager.Instance.State = PowerEyeStates[0]; // Loop to 0
        }
        else
        {
            CurrentVisualManager.Instance.State = PowerEyeStates[index + 1];
        }
    }
    private void UpdateColor(VisualState state)
    {

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
}
