using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTerrain : MonoBehaviour
{

    public VisualState requiredToBeActive;
    private SpriteRenderer SpriteTexture;

    private void OnDestroy()
    {
        if (requiredToBeActive != VisualState.COLORLESS_VIEW)
        {
            CurrentVisualManager.Instance.OnVisualStateChanged -= SwitchTerrainBasedOnColor;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // ONly listen if it can be changed
        if (requiredToBeActive != VisualState.COLORLESS_VIEW)
        {
            CurrentVisualManager.Instance.OnVisualStateChanged += SwitchTerrainBasedOnColor;
        }
        SpriteTexture = GetComponent<SpriteRenderer>();
        SwitchTerrainBasedOnColor(CurrentVisualManager.Instance.State);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckRequired(VisualState state, Color stateColor)
    {

        SpriteTexture = GetComponent<SpriteRenderer>();
        bool activated = (requiredToBeActive == state);
        if (SpriteTexture != null)
        {
            if (requiredToBeActive == VisualState.COLORLESS_VIEW)
            {
                gameObject.SetActive(true);
            }
            else
            if (activated)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }


    private void SwitchTerrainBasedOnColor(VisualState state)
    {
        switch (state)
        {
            case VisualState.RED_VIEW:
                CheckRequired(state, Color.red);
                break;

            case VisualState.GREEN_VIEW:
                CheckRequired(state, Color.green);
                break;

            case VisualState.BLUE_VIEW:
                CheckRequired(state, Color.blue);
                break;

            case VisualState.COLORLESS_VIEW:
                // Never should actually have a color, this doesnt affect 
                // main scene
                CheckRequired(state, Color.white);
                break;
        }

    }
}