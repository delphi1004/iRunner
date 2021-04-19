


//--------- This is TCP/IP client module by John Lee. 2017.09.18 ----------



using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System;
using System.Threading;



public class JLClient : MonoBehaviour
{

	//-------- private member property area ------------------------------//


	private TcpClient myClient;
    private NetworkStream myStream;
    private byte[] recvBuffer;
    private String serverIP;
    private bool goSendJump;

	


    //-------- public member property area -------------------------------//

    public bool isServerConnected;

	//-------- private member method area --------------------------------//


	private void initDefaultData()
	{
        //serverIP = "192.168.0.2";

        serverIP = "172.20.10.2";

        //serverIP = "10.100.59.32";

        connectToServer();
    }


    private void connectToServer()
    {
		isServerConnected = false;

		recvBuffer = new byte[1024];

		try
		{
			Debug.Log(("Connecting to server..."));

			myClient = new TcpClient();

			myClient.BeginConnect(serverIP, 5014, new AsyncCallback(ConnectionCallBack), myClient);
		}
		catch (System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
    }

    private void ConnectionCallBack(IAsyncResult result)
    {
        
		try
		{
			isServerConnected = myClient.Connected;

			if (isServerConnected == true)
			{
				myStream = myClient.GetStream();

				myStream.BeginRead(recvBuffer, 0, 1024, new AsyncCallback(readData), null);

				Debug.Log("Connected to server!");

                JLGlobal.Shared.brutePlayMode = PLAY_MODE.CONTROL_BRUTE;

                JLGlobal.Shared.DanceBrute.changeMode();
			}
            else
            {
                Debug.Log("Can't connect to the server!");

                Thread.Sleep(5000);

                connectToServer();
            }
		}
		catch (System.Exception ex)
		{
			Debug.Log(ex.Message);
		}

    }


    private void readData(IAsyncResult result)
    {
		string[] items;
        string[] finalData;
		string tempData;
		int recvDataSize;

        recvDataSize = myStream.EndRead(result);

		tempData = System.Text.Encoding.ASCII.GetString(recvBuffer, 0, recvDataSize);

		items = tempData.Split('\0');

        finalData = items[0].Split(' ');

        JLGlobal.Shared.brutePlayMode = (PLAY_MODE)Convert.ToInt16(finalData[0]);

        JLGlobal.Shared.DanceBrute.changeMode();
        	
        Debug.Log("Data received from Server :" + tempData);

		myStream.BeginRead(recvBuffer, 0, 1024, new AsyncCallback(readData), null);
    }


	private void sendSensorData(IAsyncResult result)
	{
        NetworkStream stream;

        stream  = (NetworkStream)result.AsyncState;

		stream.EndWrite(result);
    }


	private void sendData()
	{
		byte[] data;
		string tempData;
        float tempJump;

        tempJump = (Input.acceleration.y + Input.acceleration.z);

		if (tempJump >= -0.5 && goSendJump == true)
		{
			Debug.Log(("Jump Detected!"));

			JLGlobal.Shared.JumpBrute = true;

			goSendJump = false;
		}

		if (tempJump < -2.0f && goSendJump == false)
		{
			goSendJump = true;
		}

        tempData = string.Format("{0} {1:0.0} {2:0.0} {3:0.0} {4:0.0} {5:0.0} {6:0.0} {7}\0", (int)JLGlobal.Shared.brutePlayMode, Input.acceleration.x, Input.acceleration.y,
                                 Input.acceleration.z, Input.gyro.attitude.x, Input.gyro.attitude.y, Input.gyro.attitude.z , JLGlobal.Shared.JumpBrute);

		data = System.Text.Encoding.ASCII.GetBytes(tempData);

		myStream.BeginWrite(data, 0, data.Length, new AsyncCallback(sendSensorData), myStream);

        if (JLGlobal.Shared.JumpBrute == true)
        {
            JLGlobal.Shared.JumpBrute = false;
        }
	}


    //-------- public member method area ---------------------------------//


	public JLClient()
    {
        ;
    }


    public void Update()
    {
        if (myClient != null)
        {
            if (myClient.Connected == true)
            {
                sendData();
            }
        }
    }


	public void startClient()
    {
        initDefaultData();
    }


}
