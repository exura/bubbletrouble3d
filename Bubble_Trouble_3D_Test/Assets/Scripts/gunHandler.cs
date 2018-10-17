using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunHandler : MonoBehaviour {

	public Camera cam;

	public float range = 100f;
	public ParticleSystem bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Shoot ();
		}
	}

	void Shoot()

	{
     	bullet.Play ();
		//bullet.transform.parent = cam.transform;
		//bullet.transform.localPosition = new Vector3(0,0,7);
		bullet.transform.parent = null;
		bullet.transform.forward = cam.transform.forward;


		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, range)) { // Params: From pos of cam, in direction we are facing, what are we hitting, how far?
			Debug.Log(hit.transform.name);
		}

		//string hitname = hit.transform.name;
		//if (hitname.Equals ("Ball", System.StringComparison.Ordinal)) {
			//print("1");
			//txt.text = "Raycast Info: \n Ball hit! \n Range: ";
		}

	}

