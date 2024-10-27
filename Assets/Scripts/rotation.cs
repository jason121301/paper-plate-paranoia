using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    private float rotz;
    public float   rotationSpeed;
    public bool rotationClock;

    // Update is called once per frame
    void Update()
    {
        if(rotationClock == true)
        {
            rotz += Time.deltaTime * rotationSpeed; 
        }
        else
        {
            rotz += -Time.deltaTime * rotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotz);      

    }
}
