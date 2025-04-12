using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator animator;
    public DoorState StartingDoorState;
    private DoorState CurrentState;
    private bool InArea;
    private bool DoorOpen;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SetDoorState(StartingDoorState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetDoorState(DoorState.OPENING);
        }
        if (Input.GetKeyDown(KeyCode.G) && InArea && DoorOpen)
        {
            Debug.Log("Entered");
            // Move to next level?
        }
    }

    void PlayAnimation(string animationName, int frame, float speed)
    {

        animator.speed = speed;
        animator.Play(animationName, 0, frame);

    }

    void SetDoorState(DoorState state)
    {
        CurrentState = state;
        switch (state)
        {
            case DoorState.CLOSED:
                DoorOpen = false;
                PlayAnimation("Door_Open_Animation", 0, 0);
                break;
            case DoorState.OPENING:
                PlayAnimation("Door_Open_Animation", 0, 3);
                StartCoroutine(WaitForAnimation("Door_Open_Animation", 3));
                break;
            case DoorState.OPEN:
                // Hold Open
                DoorOpen = true;
                Debug.Log("Im Open Now");
                PlayAnimation("Door_Open_Animation", 1, 0);
                break;
        }
    }

    IEnumerator WaitForAnimation(string animationName, int speed)
    {
        // Look at all clips and find the one we are using
        foreach (var animation in animator.runtimeAnimatorController.animationClips)
        {
            if (animation.name == animationName)
            {
                yield return new WaitForSeconds(animation.length / speed);
                SetDoorState(DoorState.OPEN);

                yield break;
            }
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

    // Let the level manager open the end door
    public void OpenEndDoor()
    {
        SetDoorState(DoorState.OPENING);
    }


}
