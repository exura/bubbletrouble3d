using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	// if bullet collides with another object
	void OnTriggerEnter(Collider other) {


		// check if the other object is a ball (or specifically if it has the ball-behaviour attached to it
		ballBehaviour ball = other.GetComponent<ballBehaviour> ();

		//if it does
		if (ball != null) {

			// tell the ball to explode
			ball.Explode();
		}

		if (other.tag != "Player") {
				// either way; destroy the bullet
				Destroy (this.gameObject);
		}
	}
}
