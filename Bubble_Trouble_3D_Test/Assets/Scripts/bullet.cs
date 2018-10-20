using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public float speed = 2.0f;
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, speed * Time.deltaTime);
		
	}

	void OnTriggerEnter(Collider other) {
		ballBehaviour ball = other.GetComponent<ballBehaviour> ();
		if (ball != null) {
			ball.Active = false;
		}
		Destroy (this.gameObject);
	}
}
