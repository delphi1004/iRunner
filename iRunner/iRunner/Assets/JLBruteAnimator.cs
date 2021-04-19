//--------- This is animation of Brute module by John Lee. 2017.09.22 ----------


//-------- private member property area ------------------------------//

//-------- public member property area -------------------------------//

//-------- private member method area --------------------------------//

//-------- public member method area ---------------------------------//



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JLBruteAnimator : MonoBehaviour 
{

    //-------- private member property area ------------------------------//

    private bool animationDone;
    private GameObject objTarget;
    private float accAnimation;

	//-------- public member property area -------------------------------//

	//-------- private member method area --------------------------------//

    private void initDefaultData()
    {
        accAnimation = 500.0f;

        animationDone = false;
    }

	// Use this for initialization

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	//-------- public member method area ---------------------------------//

    public void setTargetObj(GameObject obj)
    {
        objTarget = obj;
    }

    public bool isAnimationDone()
    {
        return animationDone;
    }

    public void setAnimationDone()
    {
        initDefaultData();
    }

	public void doDisappearAnimation()
    {
        objTarget.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);

		objTarget.transform.Rotate(0, Time.deltaTime * accAnimation, 0);

		accAnimation += 50.0f;

        if (accAnimation > 5000)
        {
            animationDone = true;

            objTarget.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            objTarget.transform.position = new Vector3(objTarget.transform.position.x, 40, objTarget.transform.position.z);

            objTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            animationDone = false;
        }
    }

    public void doAppearAnimation()
    {

        ;

    }

    public void updateAnimation()
    {
        
        ;


    }




}
