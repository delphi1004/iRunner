using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class GyroTest : MonoBehaviour 
{

    Quaternion gyroRotation;
    GameObject myCube;


	private void GyroToUnity(Quaternion q)
    {
        gyroRotation.Set(q.y, 0, 0, 0);
	}


	// Use this for initialization
	void Start () 
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;

        gyroRotation = new Quaternion(0, 0, 0, 1);

        myCube = GameObject.Find("TestCube");
	}


    // Update is called once per frame
    void Update()
    {
        GyroToUnity(Input.gyro.attitude);

        myCube.transform.rotation = gyroRotation;

        myCube.transform.eulerAngles = new Vector3(-(Input.gyro.attitude.eulerAngles.y - 45), 0,0);//Input.gyro.attitude.eulerAngles.x, Input.gyro.attitude.eulerAngles.z);

        //Debug.Log(Input.gyro.attitude.eulerAngles.y + "  " + 0 + "  " + 0);

        Debug.Log(string.Format("{0:0.0}   {1:0.0}   {2:0.0}", Input.gyro.attitude.eulerAngles.y , Input.gyro.attitude.eulerAngles.z , 
                                Input.gyro.attitude.eulerAngles.x));
    }
 

}
