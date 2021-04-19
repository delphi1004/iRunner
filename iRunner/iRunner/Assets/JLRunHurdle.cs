using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JLRunHurdle : MonoBehaviour 
{

	//-------- private member property area ------------------------------//

	//-------- public member property area -------------------------------//

	//-------- private member method area --------------------------------//


    private void initDefaultData()
    {




        ;



    }

	//-------- public member method area ---------------------------------//

	
    // Use this for initialization
	void Start () 
    {
        initDefaultData();
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (transform.position.z <= JLGlobal.Shared.positionBrute.z - 10)
		{
            Destroy(transform.gameObject);
		}
	}

    void OnTriggerEnter(Collider othercollider)
    {
        if (othercollider.name == "BruteWithASkirt" && JLGlobal.Shared.isClientConnected == true)
        {
            Debug.Log("Send him to iPhone!");

            JLGlobal.Shared.brutePlayMode = PLAY_MODE.DANCE_BRUTE;

            Destroy(transform.gameObject);
        }
    }
}
