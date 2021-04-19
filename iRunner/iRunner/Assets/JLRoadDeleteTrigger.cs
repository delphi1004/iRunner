using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JLRoadDeleteTrigger : MonoBehaviour 
{

    private bool startDestroy;
    private float currentTime;

	// Use this for initialization
	void Start () 
    {
        startDestroy = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (startDestroy == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > 10.0f)
            {
                Destroy(transform.root.gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider othercollider)
    {
        startDestroy = true;
    }

}
