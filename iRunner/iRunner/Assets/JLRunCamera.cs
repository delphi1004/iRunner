using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JLRunCamera : MonoBehaviour 
{
    private GameObject brute;
    private Vector3 offset;


	// Use this for initialization
	void Start () 
    {
        JLGlobal.Shared.FollowBrute = true;

        brute = GameObject.Find("BruteWithASkirt");

        offset = transform.position - new Vector3(transform.position.x, transform.position.x, brute.transform.position.z + 0.4f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (JLGlobal.Shared.FollowBrute == true)
        {
            transform.position = brute.transform.position + offset;
        }
	}
}
