using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// the ball needs a rigidbody to work, so require it
[RequireComponent(typeof(Rigidbody))]

public class ballBehaviour : MonoBehaviour {

	// holds the level of the ball
	private int level = 1;
	private int bonus = 1;

	// Holds random number for bonus ball
	//private int rngBonus;

	//Define bonus-texture OBSOLETE but keep for now
	//public Texture bonusTexture; 

	// holds the reference to the balls rigidbody
	public Rigidbody _rigidbody;

	// Holds reference to renderer OBSOLETE but keep for now
	//public Renderer rend;



	// Use this for initialization
	void Start () {



		// initialise the level
		//level = 1;

		// and reference the rigidbody
		_rigidbody = GetComponent<Rigidbody> ();

		//rend = GetComponent<Renderer> ();



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

	// make a method that can get and set the bonus of the ball
	public int Bonus {
		get 
		{
			return bonus;

		}
		set 
		{
			bonus = value;
		}
	}

	public void GiveVelocity(Vector3 orgVel, float maxSpeed, bool canMoveY) {
		if (canMoveY) {
			Vector3 tmpVector = new Vector3 (Random.Range (-maxSpeed, maxSpeed + 1.0f), Random.Range (-maxSpeed, maxSpeed + 1.0f), Random.Range (0.0f, maxSpeed + 1.0f));
			_rigidbody.velocity = orgVel+tmpVector;
		} else {
			Vector3 tmpVector = new Vector3 (Random.Range (-maxSpeed, maxSpeed + 1.0f), Random.Range (-maxSpeed, maxSpeed + 1.0f), Random.Range (0.0f, maxSpeed + 1.0f));
			_rigidbody.velocity = orgVel+tmpVector;
		}
	}




	// Changing texture on ball OBSOLETE but keep for now
//	public void bonusBall() {
//
//			rend.material.mainTexture = bonusTexture; // Change texture to assigned bonusTexture (inspector)
//
//	}
}
