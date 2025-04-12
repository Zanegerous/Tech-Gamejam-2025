using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCheks : MonoBehaviour
{
    public Roborto isHeOnGround;
    public Text GroundedCheck;
    //public Text GroundedCheck2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeOnGround == true)
        {
            GroundedCheck.text = "Yes.";
        }
        else if (isHeOnGround == false) {
            GroundedCheck.text = "No.";
        }
        else
        {
            GroundedCheck.text = "Err";
        }
    }
}
