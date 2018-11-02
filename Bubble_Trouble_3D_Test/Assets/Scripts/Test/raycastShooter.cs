using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raycastShooter : MonoBehaviour {

	public float range = 100f;

	public Camera cam;

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
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, range)) { // Params: From pos of cam, in direction we are facing, what are we hitting, how far?
			Debug.Log(hit.transform.name);
		}

		string hitname = hit.transform.name;
		if (hitname.Equals ("Ball", System.StringComparison.Ordinal)) {
			print("1");

			print ("Raycast Info:");
			print ("Ball hit!");
			print("Range:");
		}

		}
	}

