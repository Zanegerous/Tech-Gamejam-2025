using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    public float rotationSpeed = 60f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
