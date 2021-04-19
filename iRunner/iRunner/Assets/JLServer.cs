

//--------- This is TCP/IP communication class by John Lee. 2017.09.13 -----------//


using UnityEngine;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System;


public class JLServer 
{
    

    //---------- private member property area ---------------------//

    private TcpListener myServer;
    private Socket clientSocket;
    private Vector3 accData;
    private byte[] recvBytes;



	//---------- public member property area ----------------------//




	//---------- private member method area -----------------------//




    private void initDefaultData()
    {
        IPHostEntry host;

        accData  = new Vector3(0, 0, 0);

        host = Dns.GetHostEntry(Dns.GetHostName());

        //foreach (String ip in (String)host.AddressList)
        {
         //   Debug.Log(ip);
        }

        myServer = new TcpListener(host.AddressList[2],5014);

        myServer.Start();

        StartListeningForConnections();

        Debug.LogFormat("Server started... {0}", myServer.ToString());
    }

	private void StartListeningForConnections()
	{
        myServer.BeginAcceptSocket(acceptNewClient, myServer);

		Debug.Log("SERVER ACCEPTING NEW CLIENTS");
	}

    private void processRemoteData(string recvData)
    {
        //Debug.Log("Data received... " + recvData);

        //myCube.transform.Rotate(0,100,0);

        //gyroData.y = Random.Range(0, 360);
    }

	private void acceptNewClient(System.IAsyncResult asyncResult)
	{
		clientSocket = null;

		recvBytes = new byte[1024];

		try
		{
			clientSocket = myServer.EndAcceptSocket(asyncResult);

            JLGlobal.Shared.isClientConnected = clientSocket.Connected;
		}
		catch (System.Exception ex)
		{
			Debug.LogError(string.Format("Exception on new socket: {0}", ex.Message));
		}

		clientSocket.NoDelay = true;
	
		BeginReceiveData();

		StartListeningForConnections();
	}


	private void BeginReceiveData()
	{
		clientSocket.BeginReceive(recvBytes, 0, recvBytes.Length, SocketFlags.None, EndReceiveData, null);
	}


	private void EndReceiveData(System.IAsyncResult iar)
	{
        string[] items;
        string[] finalData;
        string tempData;
        int recvDataSize;

        recvDataSize = clientSocket.EndReceive(iar);

		tempData = System.Text.Encoding.ASCII.GetString(recvBytes, 0, recvDataSize);

        items = tempData.Split('\0');

        //Debug.Log(tempData);

        for (int i = 0; i < items.Length-1;i++)
        {
            finalData = items[i].Split(' ');

            if (finalData.Length == 8)
            {
                if (JLGlobal.Shared.brutePlayMode == PLAY_MODE.PAUSE && ((PLAY_MODE)Convert.ToInt16(finalData[0])) == PLAY_MODE.PREPARE_RESUME)
                {
                    JLGlobal.Shared.brutePlayMode = PLAY_MODE.PREPARE_RESUME;

                    Debug.Log(tempData);
                }

                accData.x = (float)Convert.ToDouble(finalData[1]);
                accData.y = (float)Convert.ToDouble(finalData[2]);
                accData.z = (float)Convert.ToDouble(finalData[3]);

                JLGlobal.Shared.JumpBrute = Convert.ToBoolean(finalData[7]);

                if (JLGlobal.Shared.JumpBrute == true)
                {
                    Debug.Log("Received jump Brute!");
                }

                JLGlobal.Shared.AccData = accData;
            }
		}

		BeginReceiveData();
	}


    private void sendBruteCallBack(System.IAsyncResult iar)
    {
        int bytesSent;

        try
        {
            Socket handler = (Socket)iar.AsyncState;

            bytesSent = handler.EndSend(iar);

            Debug.Log(string.Format("Sent {0} bytes to client.", bytesSent));
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


	void Start()
	{
        
        ;

	}

	void Update()
	{
        
        ;

	}


	//---------- public member area -------------------------------//


	public JLServer()
    {
        initDefaultData();
    }

    public void sendModeChanged()
    {
        string data;
        byte[] byteData;

        data = string.Format("{0} {1:0.0} {2:0.0} {3:0.0} {4:0.0} {5:0.0} {6:0.0}\0", (int)JLGlobal.Shared.brutePlayMode, 0, 0, 0, 0, 0, 0);

        Debug.Log("Send him to iPhone! " + data);

		byteData = System.Text.Encoding.ASCII.GetBytes(data);

        if (clientSocket != null)
        {
            if (clientSocket.Connected == true)
            {
                clientSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(sendBruteCallBack), clientSocket);
            }
        }
    }


	


    //----------- event handling area -----------------------------//


}
