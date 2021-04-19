using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JLItemRunner : MonoBehaviour 
{
    private bool shouldAnimate;
    private float curAngle;
    private ParticleSystem particle;
    private AudioSource audio;
     

	// Use this for initialization
	void Start () 
    {
        shouldAnimate = true;

        curAngle = 0.0f;

        this.GetComponent<Renderer>().enabled = true;

        particle = this.gameObject.GetComponentInChildren<ParticleSystem>();

        particle.Stop();

        audio = this.gameObject.GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if (shouldAnimate == true)
        {
            transform.eulerAngles = new Vector3(0, curAngle, 0);

            curAngle += 5;
        }

        if (transform.position.z <= JLGlobal.Shared.positionBrute.z - 10)
        {
            Destroy(transform.gameObject);
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (particle != null)
        {
            particle.Play();
        }

        this.GetComponent<Renderer>().enabled = false;

        StartCoroutine(destoryItem());
    }


    //--------  public member method area ----------------------------//


    IEnumerator destoryItem()
    {
        yield return new WaitForSeconds(2);

        Destroy(transform.gameObject);
    }


}
