using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public Transform InteractorSource;
    public float InteractRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
    // Update is called once per frame
//     void Update()
//     {
//         if(Input.GetKeyDown(KeyDown.E)){
//             Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
//             if(Physics.Raycast(r, out RayCastHit hitInfo, InteractRange)){
//                 if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)){
//                     interactObj.Interact();
//                 }
//             }
//         }
        
//     }
// }
