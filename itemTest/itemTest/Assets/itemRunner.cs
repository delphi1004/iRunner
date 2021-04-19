using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemRunner : MonoBehaviour 
{
    private bool shouldAnimate;
    private float curAngle;
 

	// Use this for initialization
	void Start () 
    {
        shouldAnimate = true;

        curAngle = 0.0f;

	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if (shouldAnimate == true)
        {
            transform.eulerAngles = new Vector3(0, curAngle, 0);

            curAngle += 5;
        }

	}


}
