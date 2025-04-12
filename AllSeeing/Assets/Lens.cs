using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens : MonoBehaviour, IInteractable
{
    public bool isOpened{get; private set;}
    public string LensID{get; private set;}
    public GameObject itemPrefab; // powers lens give
    public Sprite openedSprite;
     private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start(){
        LensID ??= GlobalHelper.GenerateUniqueID(gameObject);
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    
    public bool CanInteract(){
       return !isOpened;
    }

    public void Interact(){
       if (!CanInteract()) return;

       grabLens();

       Debug.Log($"Lens {LensID} activates!");
    }

    public void grabLens(){
        SetLens(true);

        // swap item
        if (itemPrefab){
            Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
    
        }
    }
    private void SetLens(bool opened){
        isOpened = opened;
        if (isOpened && openedSprite != null && spriteRenderer != null){
            spriteRenderer.sprite = openedSprite;
        }
    }
}