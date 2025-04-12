using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTerrain : MonoBehaviour
{

    public VisualState requiredToBeActive;
    private SpriteRenderer SpriteTexture;
    private Collider2D objectCollider;



    // Activated or deactivates the listener to the currentVisualManager that tracks the worlds color state
    private void OnEnable()
    {
        CurrentVisualManager.Instance.OnVisualStateChanged += SwitchTerrainBasedOnColor;
    }

    private void OnDisable()
    {
        CurrentVisualManager.Instance.OnVisualStateChanged -= SwitchTerrainBasedOnColor;
    }


    // Start is called before the first frame update
    void Start()
    {
        SpriteTexture = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckRequired(VisualState state, Color stateColor)
    {
        SpriteTexture = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();
        bool activated = (requiredToBeActive == state);
        if (SpriteTexture != null)
        {
            if (activated)
            {
                SpriteTexture.color = stateColor;
            }
            else
            {
                // have to do it this way because gameObject.SetActive(false) unsubscribes it.
                // We may want to change the onDisable and handle it differently, but for now
                // Im just doing this for testing purposes

                objectCollider.enabled = activated;
                objectCollider.isTrigger = activated;

                SpriteTexture.color = Color.clear;


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
                CheckRequired(state, Color.magenta);
                break;
        }

    }
}
