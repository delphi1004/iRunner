using UnityEngine;
using System.Collections;



public class JLRunBrute : MonoBehaviour 
{

	//-------- private member property area ------------------------------//


	private Rigidbody thisRigid;

    private int controlSpeed;
    private float posBrute;
    private Animator bruteAnimator;
    private JLBruteAnimator bruteDisappearAnimator;
    private int jumpCount;
    private bool jumpBrute;


	//-------- public member property area -------------------------------//


	public float speed;



	//-------- private member method area --------------------------------//


    private void initDefaultData()
    {
        bruteDisappearAnimator = new JLBruteAnimator();

        bruteDisappearAnimator.setTargetObj(this.gameObject);
                                            
        JLGlobal.Shared.startServerMode();

        bruteAnimator = gameObject.GetComponent<Animator>();

        bruteAnimator.speed = 0.0f;
           
        controlSpeed = 5;

        JLGlobal.Shared.JumpBrute = false;

        jumpBrute = false;
    }


    private void runBrute()
    {
		speed = (9.5f + JLGlobal.Shared.AccData.y * controlSpeed);

		if (speed <= 0)
		{
			speed = 0.0f;
		}

		bruteAnimator.speed = speed / 4.5f;

		posBrute = JLGlobal.Shared.AccData.x;

        if (JLGlobal.Shared.JumpBrute == true)
        {
            jumpBrute = true;
        }

        if (jumpBrute == true)
        {
            thisRigid.velocity = new Vector3(posBrute * (controlSpeed * 3), (float)10.0f, speed);

            jumpCount++;

            if (jumpCount > 25)
            {
                jumpBrute = false;
            }
        }
        else
        {
            if (jumpCount != 0)
            {
               thisRigid.velocity = new Vector3(posBrute * (controlSpeed * 3), (float)-10.0f, speed);

               jumpCount--;
            }
            else
            {
                thisRigid.velocity = new Vector3(posBrute * (controlSpeed * 3), (float)-0.4f, speed);
            }

        }

        JLGlobal.Shared.positionBrute = thisRigid.position;
    }

	
	// Use this for initialization
	void Start () 
    {
		thisRigid = GetComponent<Rigidbody>();

        initDefaultData();
	}

    // Update is called once per frame
    void Update()
    {

        if (JLGlobal.Shared.isClientConnected == true)
        {
            if (JLGlobal.Shared.brutePlayMode == PLAY_MODE.CONTROL_BRUTE)
            {
                runBrute();
            }

            if (JLGlobal.Shared.brutePlayMode == PLAY_MODE.DANCE_BRUTE)
            {
                JLGlobal.Shared.FollowBrute = false;

                bruteAnimator.speed = 0.0f;

                if (bruteDisappearAnimator.isAnimationDone() == false)
                {
                    thisRigid.useGravity = false;

                    bruteDisappearAnimator.doDisappearAnimation();
                }
                else
                {
                    JLGlobal.Shared.sendBruteMode();

                    JLGlobal.Shared.brutePlayMode = PLAY_MODE.PAUSE;

                    bruteDisappearAnimator.setAnimationDone();
                }

            }

            if (JLGlobal.Shared.brutePlayMode == PLAY_MODE.PREPARE_RESUME)
            {
                JLGlobal.Shared.brutePlayMode = PLAY_MODE.PREPARE_RUNAGAIN;
                
                thisRigid.useGravity = true;

                StartCoroutine(resumeRunning());
            }

        }

    }



    //-------- public member method area ---------------------------------//

    IEnumerator resumeRunning()
	{
        Debug.Log("Waiting for brute come back");
             
		yield return new WaitForSeconds(3);

        JLGlobal.Shared.FollowBrute = true;

        JLGlobal.Shared.brutePlayMode = PLAY_MODE.CONTROL_BRUTE;
	}

}