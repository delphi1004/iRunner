using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JLItemGenerator : MonoBehaviour 
{




    //-------- private member property area ------------------------------//



    private float curZpos;
	private GameObject prefab;
    private GameObject prefabHurdle;
    private int numberOfObjects;
    private float itemInterval;




	//-------- public member property area -------------------------------//




	//-------- private member method area --------------------------------//




    private void createHurdle(Vector3 pos)
    {
		if (Random.Range(0, 20) == 5)
		{
			Instantiate(prefabHurdle, pos, Quaternion.identity);
		}
    }


    private void createType1()
    {
		float angle;
        float radius;
        Vector3 pos;

		numberOfObjects = 20;

		radius = 5.0f;

        prefab = (GameObject)Resources.Load("Prefabs/watermelon");

        Debug.Log(prefab);

		for (int i = 0; i < numberOfObjects; i++)
		{
			angle = i * Mathf.PI * 1 / numberOfObjects;

            curZpos += itemInterval;

            pos = new Vector3(Mathf.Cos(angle) * radius, transform.position.y + 1, curZpos);

			Instantiate(prefab, pos, Quaternion.identity);

            createHurdle(pos);
		}

        curZpos += itemInterval *3;
    }


    private void createType2()
    {
		float angle;
		float radius;
		Vector3 pos;

		numberOfObjects = 20;

        radius = 0.1f;

		prefab = (GameObject)Resources.Load("Prefabs/cake");

		for (int i = 0; i < numberOfObjects; i++)
		{
			angle = i * Mathf.PI * 1 / numberOfObjects;

            curZpos += itemInterval;

			pos = new Vector3(Mathf.Cos(angle) * radius, transform.position.y + 1, curZpos);

			Instantiate(prefab, pos, Quaternion.identity);

            createHurdle(pos);
		}

        curZpos += itemInterval * 3;
    }


    private void createType3()
    {
		float angle;
		float radius;
		Vector3 pos;

        pos = new Vector3(0, 0, 0);

		numberOfObjects = 20;

		radius = 2.0f;

		prefab = (GameObject)Resources.Load("Prefabs/watermelon");

		Debug.Log(prefab);

		for (int i = 0; i < numberOfObjects; i++)
		{
            angle = -(i * Mathf.PI * 1 / numberOfObjects);

			curZpos += itemInterval;

			pos = new Vector3(Mathf.Cos(angle) * radius, transform.position.y + 1, curZpos);

			Instantiate(prefab, pos, Quaternion.identity);

            createHurdle(pos);
		}

		curZpos += itemInterval * 3;
    }


    private void initDefaultData()
    {
        int index;

        index = 0;

        itemInterval = 2;

        curZpos = transform.position.z;

        prefabHurdle = (GameObject)Resources.Load("Prefabs/Hurdle01");

        for (int i = 0; i < 3;i++)
        {
            index = Random.Range(0, 3);

            Debug.Log("item sequence = "+index);

            switch(index)
            {
                case 0: createType1();break;
                case 1: createType2(); break;
                case 2: createType3(); break;
            }
     
        }

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
        ;	
	}


}
