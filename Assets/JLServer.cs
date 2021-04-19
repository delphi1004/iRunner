

//--------- This is TCP/IP communication class by John Lee. 2017.09.13 -----------//


using UnityEngine;
using System.Net;
using System.Threading;
using System.Net.Sockets;


public class JLServer : MonoBehaviour 
{
    

    //---------- private member property area ---------------------//

    private TcpListener myServer;
    private Socket clientSocket;
    private IPAddress myAddress;
    private bool isServerGo;
    private GameObject myCube;
    private Vector3 gyroData;
    private byte[] recvBytes;


	//---------- public member property area ----------------------//




	//---------- private member method area -----------------------//



    private void initDefaultData()
    {
        isServerGo = true;

        gyroData = new Vector3(0, 0, 0);

        myCube = GameObject.Find("TestCube");

        //myAddress = IPAddress.Parse("192.168.0.2");

        myAddress = IPAddress.Parse("10.100.61.27");

        myServer = new TcpListener(myAddress,6000);

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

        gyroData.y = Random.Range(0, 360);
    }

  
	private void acceptNewClient(System.IAsyncResult asyncResult)
	{
		clientSocket = null;

		recvBytes = new byte[1024];

		try
		{
			clientSocket = myServer.EndAcceptSocket(asyncResult);
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

        for (int i = 0; i < items.Length-1;i++)
        {
            finalData = items[i].Split(' ');

            gyroData.x = -(System.Convert.ToInt16(finalData[2]) - 45);
            gyroData.y = -(System.Convert.ToInt16(finalData[1]));
			gyroData.z = (System.Convert.ToInt16(finalData[0]));
        }

		BeginReceiveData();
	}




	//---------- public member area -------------------------------//


	void Start()
	{
		initDefaultData();
	}

    void Update()
    {
        myCube.transform.eulerAngles = gyroData;
	}





    //----------- event handling area -----------------------------//




}
