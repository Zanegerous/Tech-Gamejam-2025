using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour{

    private IInteractable interactableinRange = null; // closest interactable
    public GameObject interactionIcon;
    // Start is called before the first frame update
    void Start(){
        interactionIcon.SetActive(false);   
    }

    public void OnInteract(InputAction.CallbackContext context){
        if(context.performed){
            interactableinRange?.Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract()){
            interactableinRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableinRange){
            interactableinRange = null;
            interactionIcon.SetActive(false);
        }
    }
}