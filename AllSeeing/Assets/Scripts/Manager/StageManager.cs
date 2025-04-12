using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public VisualState levelStartState;
    public float LevelTimeMinutes;
    
    // Level setup
    void Start()
    {  
        Debug.Log("Loaded");
        // CurrentVisualManager.Instance.State = levelStartSstate;
        StartCoroutine(LevelTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LevelTimer()
    {

        yield return new WaitForSeconds(5); // Waits
        
        // what you want to repeat

        StartCoroutine(LevelTimer()); // restarts
    }
    IEnumerator GameOverClock()
    {

        yield return new WaitForSeconds(LevelTimeMinutes * 60f); // Waits minutes
        Debug.Log("Level Timer Ended");
        // run game over/player death script.
    }
}
