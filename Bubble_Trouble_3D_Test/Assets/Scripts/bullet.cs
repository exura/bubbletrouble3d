using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	// if bullet collides with another object
	void OnTriggerEnter(Collider other) {
		ballBehaviour ball = other.GetComponent<ballBehaviour> ();
		if (ball != null) {
			ball.Active = false;
		}

		// destroy the bullet
		Destroy (this.gameObject);
	}
}
