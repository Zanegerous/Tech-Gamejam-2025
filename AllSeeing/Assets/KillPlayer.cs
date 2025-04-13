using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour{

    public GameObject player;
    public Transform respawnPoint;
    public float fallThreshold = -10f;

    // Update is called once per frame
    void Update(){
        if(player.transform.position.y < fallThreshold){
            RespawnPlayer();
        }
    }


    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            RespawnPlayer();
        }
    }

       private void RespawnPlayer(){
        player.transform.position = respawnPoint.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // reset velocity so they don't keep falling
    }
}
