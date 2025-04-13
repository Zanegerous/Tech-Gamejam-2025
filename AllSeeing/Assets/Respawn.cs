using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = SpawnPoint.position;
    }

    public void SetRespawnPosition() 
    {
        //transform.SpawnPoint = newSpawn.position;
    }

    public void RespawnPlayer() 
    {
        transform.position = SpawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        // var respawnKey = Input.GetButtonDown("Previous");

        // if (Input.GetButtonDown("Previous"))
        // {
        //     RespawnPlayer();
        // }
    }
}
