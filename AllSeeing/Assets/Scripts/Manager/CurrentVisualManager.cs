using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentVisualManager : MonoBehaviour
{
    // Singleton Pattern with Event/Delegates to call
    public static CurrentVisualManager Instance { get; private set; }
    public event Action<VisualState> OnVisualStateChanged;

    private VisualState _State;
    public VisualState State
    {
        get => _State; // returns state
        set // updates the state if different and sets all listeners to new state.
        {
            if (_State != value)
            {
                _State = value;
                // calls all the listening functions with the _state sent to it
                OnVisualStateChanged?.Invoke(_State);
            }
        }
    }

    // Makes the manager before the scene loads, this fixes the issue of awake coming after some stuff
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void MakeTheScriptOnStart()
    {
        if (Instance == null)
        {
            // creates the manager script 
            GameObject manager = new GameObject("CurrentVisualManager");
            manager.AddComponent<CurrentVisualManager>();
        }
    }

    private void Awake()
    {
        //print("I woke up");
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
