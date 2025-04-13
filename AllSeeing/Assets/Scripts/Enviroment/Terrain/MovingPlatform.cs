using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float seconds = 1f;
    public bool loop;
    private int targetPointPos = 0;
    private bool forward = true;
    private bool waiting = false;

    void Update()
    {
        Transform target = waypoints[targetPointPos];
        Vector3 current = transform.position;
        Vector3 goal = target.position;

        // Wont work without 2 points or if waiting true, 
        // /else it would repetedly call the coroutine start
        if (waiting == true || waypoints.Length < 2)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(current, goal, speed * Time.deltaTime);

        float distance = Vector3.Distance(current, goal);

        if (distance < 0.1f)
        {
            StartCoroutine(WaitAndMoveNext());
        }
    }

    IEnumerator WaitAndMoveNext()
    {
        waiting = true;
        yield return new WaitForSeconds(seconds);

        if (loop == true)
        {
            targetPointPos++;
            if (targetPointPos >= waypoints.Length)
            {
                targetPointPos = 0;
            }
        }
        else if (forward == true)
        {
            // move from 0 to max points
            targetPointPos++;
            if (targetPointPos >= waypoints.Length)
            {
                targetPointPos = waypoints.Length - 2;
                forward = false;
            }
        }
        else
        {
            // reverse from maz to zero
            targetPointPos--;
            if (targetPointPos < 0)
            {
                targetPointPos = 1;
                forward = true;
            }
        }

        waiting = false;
    }
}
