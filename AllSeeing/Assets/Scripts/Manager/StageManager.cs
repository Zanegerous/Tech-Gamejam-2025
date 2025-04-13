using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public VisualState levelStartState;
    public float LevelTimeMinutes;
    public List<ButtonScript> buttons;
    public List<LeverScript> levers;
    public NewBehaviourScript door;

    private bool EndDoorOpen = false;
    // Level setup
    void Start()
    {
        // Debug.Log("Loaded");
        // CurrentVisualManager.Instance.State = levelStartSstate;
        StartCoroutine(LevelTimer());
        StartCoroutine(GameOverClock());
        StartCoroutine(CheckCompletion());
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
        // Debug.Log("Level Timer Ended");
        // run game over/player death script.
    }

    IEnumerator CheckCompletion()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f); // Check every half second

            if (AllInputsActive() == true && EndDoorOpen == false)
            {
                // Debug.Log("All inputs active. Opening door.");
                door.OpenEndDoor();
                EndDoorOpen = true;
            }
            else if (!AllInputsActive() && EndDoorOpen)
            {
                // Debug.Log("Closing door since inputs no longer active");
                door.CloseEndDoor();
                EndDoorOpen = false;
            }
        }
    }

    bool AllInputsActive()
    {
        // These can skip empty lists without a reference error, so use can be done for few buttons
        foreach (var button in buttons)
        {
            if (!button.ButtonPressed)
            {
                return false;
            }

        }

        foreach (var lever in levers)
        {
            if (!lever.LeverOn)
            {
                return false;
            }
        }

        return true;
    }

}
