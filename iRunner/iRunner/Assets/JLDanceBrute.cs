//----- This is iPhone module by John Lee. 2017.09.18  ---------------//



//-------- private member property area ------------------------------//

//-------- public member property area -------------------------------//

//-------- private member method area --------------------------------//

//-------- public member method area ---------------------------------//



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JLDanceBrute : MonoBehaviour 
{


    //-------- private member property area ------------------------------//

 
    private Animator bruteAnimator;
    private JLClient myClient;
    private GameObject objCurtain;
    private Queue myQueue;
    private int queueData;




	//-------- public member property area -------------------------------//



	//-------- private member method area --------------------------------//


	private void initDefaultData()
	{
        myQueue = new Queue();

        objCurtain = GameObject.Find("PlaneCurtain");

        JLGlobal.Shared.startClientMode();  // start this scene as client

        bruteAnimator = GetComponent<Animator>();

        bruteAnimator.speed = 0.0f;

        JLGlobal.Shared.DanceBrute = this;

        myClient = JLGlobal.Shared.getClient();
	}

    private void trasitControlMode()
    {
        bruteAnimator.speed = 0.0f;

        objCurtain.transform.position = new Vector3(objCurtain.transform.position.x, 0, objCurtain.transform.position.z);

        JLGlobal.Shared.brutePlayMode = PLAY_MODE.PREPARE_RESUME;

        Debug.Log("in trasitControlMode " + JLGlobal.Shared.brutePlayMode);

        myClient.Update();

        JLGlobal.Shared.brutePlayMode = PLAY_MODE.CONTROL_BRUTE;

        Debug.Log("in trasitControlMode " + JLGlobal.Shared.brutePlayMode);
    }

    private void transitDanceMode()
    {
        objCurtain.transform.position = new Vector3(objCurtain.transform.position.x, 20, objCurtain.transform.position.z);

        bruteAnimator.speed = 1.0f;

        JLGlobal.Shared.brutePlayMode = PLAY_MODE.DANCE_BRUTE;

        Debug.Log("in transitDanceMode " + JLGlobal.Shared.brutePlayMode);
    }

    private void checkSwipe()
    {
        Vector2 touchDeltaPosition;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (touchDeltaPosition.x > 50)
            {
                trasitControlMode();

                Debug.Log(touchDeltaPosition);
            }
        }

        if (Input.GetMouseButtonUp(0) == true)
        {
            trasitControlMode();
        }
    }

  
    // Use this for initialization
    void Start()
	{
		initDefaultData();
	}


	// Update is called once per frame
	void Update()
	{
        if (myQueue.Count > 0)
        {
            queueData = (int)myQueue.Dequeue();

            Debug.Log("Queue " + queueData);

            if (queueData == (int)PLAY_MODE.CONTROL_BRUTE)
            {
                trasitControlMode();
            }

            if (queueData == (int)PLAY_MODE.DANCE_BRUTE)
			{
				transitDanceMode();
			}

        }

        if (JLGlobal.Shared.brutePlayMode == PLAY_MODE.DANCE_BRUTE)
        {
            checkSwipe();
        }

		if (JLGlobal.Shared.brutePlayMode == PLAY_MODE.CONTROL_BRUTE)
		{
			myClient.Update();
		}

	}



    //-------- public member method area ---------------------------------//



    public void changeMode()
    {
        myQueue.Enqueue(JLGlobal.Shared.brutePlayMode);
    }


	




    //-------- event handling method area --------------------------------//

	


}
