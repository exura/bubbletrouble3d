// Script to handle player capsule collider (trigger) i.e. when a player gets hit by any ball
// !!!Attached to GameObject Player!!!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrigger : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Method to check if any rigidbody (ball) hits the capsule collider of the player.
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ball") {							// Only check collision with objects with correct Tag
			DelegatesAndEvents.ballHitPlayer (this.gameObject); // Trigger event.
		}

		if (other.tag == "PickUp") {							// Only check collision with objects with correct Tag
			Destroy(other.transform.parent.gameObject);
		}

	}
		
}
