using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the ball needs a rigidbody to work, so require it
[RequireComponent(typeof(Rigidbody))]

public class ballBehaviour : MonoBehaviour {

	// holds the level of the ball
	private int level;

	// holds the reference to the balls rigidbody
	public Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {

		// initialise the level
		level = 1;

		// and reference the rigidbody
		_rigidbody = GetComponent<Rigidbody> ();
	}
	
	// make a public method that can add force to the rigidbody
	public void Push(Vector3 push) {
		_rigidbody.AddForce (push);
	}

	// make a public method that can trigger the ball to be destroyed
	public void Explode() {

		// this triggers the event that a ball is destroyed
		DelegatesAndEvents.BallDestroyed(this.gameObject);
	}

	// make a method that can get and set the level of the ball
	public int Level {
		get 
		{
			return level;
		}
		set 
		{
			level = value;
		}
	}
}
