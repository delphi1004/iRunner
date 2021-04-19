using UnityEngine;
using System.Collections;

public class roadTrigger : MonoBehaviour 
{
    private bool createRoadDone;
    
  	public Transform roadPrefab;


	// Use this for initialization
	void Start () 
    {
        createRoadDone = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        ;
	}


	void OnTriggerEnter (Collider othercollider)
    {
        if (createRoadDone == false)
        {
            Instantiate(roadPrefab, new Vector3(0, 1.1f, transform.parent.position.z + 154f), roadPrefab.rotation);

            createRoadDone = true;
        }

        Destroy(this.gameObject);
	}


}
