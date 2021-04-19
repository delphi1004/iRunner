using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGenerator : MonoBehaviour 
{
    private GameObject prefab;

	public int numberOfObjects = 20;
	public float radius = 5f;

	// Use this for initialization
	
    void Start () 
    {
        float angle;
        Vector3 pos;

		prefab = (GameObject)Resources.Load("Prefabs/watermelon");

        Debug.Log(prefab);
           
		for (int i = 0; i < numberOfObjects; i++)
		{
			angle = i * Mathf.PI * 5 / numberOfObjects;

            pos = new Vector3(Mathf.Cos(angle), transform.position.y +1, i * 2);

			Instantiate(prefab, pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () 
    {
        ;	
	}


}
