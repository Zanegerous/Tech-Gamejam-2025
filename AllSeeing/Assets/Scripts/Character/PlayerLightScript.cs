using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightScript : MonoBehaviour
{
    private Light2D playerLight;
    // Start is called before the first frame update
    private void OnDestroy()
    {
        CurrentVisualManager.Instance.OnVisualStateChanged -= UpdateColor;
    }

    private void Start()
    {
        playerLight = GetComponent<Light2D>();
        CurrentVisualManager.Instance.OnVisualStateChanged += UpdateColor;
    }

    private void UpdateColor(VisualState state)
    {
        playerLight.intensity = 1f; // For darkening the scene before death like Zoe suggested
        switch (state)
        {
            case VisualState.COLORLESS_VIEW:
                playerLight.color = Color.grey;

                break;
            case VisualState.RED_VIEW:
                playerLight.color = Color.red;
                break;
            case VisualState.GREEN_VIEW:
                playerLight.color = Color.green;
                break;
            case VisualState.BLUE_VIEW:
                playerLight.color = Color.blue;
                break;
        }

    }
}
