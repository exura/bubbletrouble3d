using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// the ball needs a rigidbody to work, so require it
[RequireComponent(typeof(Rigidbody))]

public class ballBehaviour : MonoBehaviour {

	// holds the level of the ball
	private int level = 1;
	private int bonus = 1;

	private float rotX = 0f;
	private float rotY = 0f;
	private float rotZ = 0f;

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

		rotX = Random.Range (-2.0f, 2.0f);
		rotY = Random.Range (-2.0f, 2.0f);
		rotZ = Random.Range (-2.0f, 2.0f);

		// initialise the level
		//level = 1;

		// and reference the rigidbody
		_rigidbody = GetComponent<Rigidbody> ();

		//rend = GetComponent<Renderer> ();



	}

	void Update() {
		transform.Rotate(rotX,rotY,rotZ);
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
			Vector3 tmpVector = new Vector3 (Random.Range (-maxSpeed, maxSpeed + 1.0f), Random.Range (maxSpeed/2.0f, maxSpeed + 1.0f), Random.Range (-maxSpeed, maxSpeed + 1.0f));
			_rigidbody.velocity = orgVel+tmpVector;
		} else {
			Vector3 tmpVector = new Vector3 (Random.Range (-maxSpeed, maxSpeed + 1.0f), 0.0f, Random.Range (-maxSpeed, maxSpeed + 1.0f));
			_rigidbody.velocity = orgVel+tmpVector;
		}
	}

	public Vector3 GetVelocity() {
		return _rigidbody.velocity;
	}

	public void SetVelocity(Vector3 vel) {
		_rigidbody.velocity = vel;
	}




	// Changing texture on ball OBSOLETE but keep for now
//	public void bonusBall() {
//
//			rend.material.mainTexture = bonusTexture; // Change texture to assigned bonusTexture (inspector)
//
//	}
}
