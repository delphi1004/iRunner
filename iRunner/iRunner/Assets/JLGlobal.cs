
//--------- This is Global module by John Lee. 2017.09.18 ----------


//-------- private member property area ------------------------------//

//-------- public member property area -------------------------------//

//-------- private member method area --------------------------------//

//-------- public member method area ---------------------------------//



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAY_MODE
{
    CONTROL_BRUTE,
    DANCE_BRUTE,
    PAUSE,
    PREPARE_RESUME,
    PREPARE_RUNAGAIN
};

public class JLGlobal 
{
    private static JLGlobal instance;
	private Vector3 accSensorData;
	private Vector3 gyroSensorData;
    private JLDanceBrute danceBrute;
    private Vector3 posBrute;
    private PLAY_MODE playMode;
    private JLServer myServer;
    private JLClient myClient;
    private bool clientConnected;
    private bool followBrute;
    private bool jumpBrute;


    public bool serverOwnMode;



	// Use this for initialization
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        ;
	}

    public void startClientMode()
    {
        playMode = PLAY_MODE.PAUSE;

		myClient = new JLClient();

		myClient.startClient();
                            
        Debug.Log((int)playMode);
    }

    public void startServerMode()
    {
		myServer = new JLServer();

		playMode = PLAY_MODE.CONTROL_BRUTE;

		accSensorData = new Vector3(0, -2, 0);
		gyroSensorData = new Vector3(0, 0, 0);

		Debug.Log((int)playMode);
    }

    public JLDanceBrute DanceBrute
    {
        get
        {
            return danceBrute;
        }

        set
        {
            danceBrute = value;

        }
    }

    public JLClient getClient()
    {
        return myClient;
    }

    public Vector3 AccData
    {
        get
        {
            return accSensorData;
        }

        set
        {
            accSensorData = value;
        }
    }

	public Vector3 gyroData
	{
		get
		{
			return gyroSensorData;
		}

		set
		{
			gyroSensorData = value;
		}
	}

    public Vector3 positionBrute
    {
        get
        {
            return posBrute;
        }

        set
        {
            posBrute = value;
        }
    }


	public PLAY_MODE brutePlayMode
	{
		get
		{
            return playMode;
		}

		set
		{
            playMode = value;

            Debug.Log("Mode Changed : " + playMode);
		}
	}

    public bool isClientConnected
    {
        get
        {
            return clientConnected;
        }

        set
        {
            clientConnected = value;
        }
    }

    public bool FollowBrute
    {
        get
        {
            return followBrute;
        }

        set
        {
            followBrute = value;
        }
    }

	public bool JumpBrute
	{
		get
		{
			return jumpBrute;
		}

		set
		{
			jumpBrute = value;
		}
	}

    public void sendBruteMode()
    {
        if (myServer != null)
        {
            myServer.sendModeChanged();
        }
    }

	public static JLGlobal Shared
	{
		get
		{
			if (instance == null)
			{
				instance = new JLGlobal();
			}

			return instance;
		}
	}
}
