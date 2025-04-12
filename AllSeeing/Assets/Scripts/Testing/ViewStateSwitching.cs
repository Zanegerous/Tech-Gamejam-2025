using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewStateSwitching : MonoBehaviour
{
    private VisualState worldState;

    // Start is called before the first frame update

    void Start()
    {
        worldState = VisualState.RED_VIEW;
        CurrentVisualManager.Instance.State = VisualState.RED_VIEW;
        InvokeRepeating("CycleStates", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

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
