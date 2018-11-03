using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using a character controller to move the character in the script, therefore required component
[RequireComponent(typeof(CharacterController))]

// Adds the script to the component menu under "Control Script" - "FPS Input" - not necessary now but perhaps in the future with more complex projects
[AddComponentMenu("Control Script/FPS Input")]


public class fpsInput : MonoBehaviour {

	//Force Unity to serialize a private field, that would otherwise be hidden

	// the prefab for the bullet emitter 
	[SerializeField] private GameObject bulletEmitter; 

	// the prefab for the bullet
	[SerializeField] private GameObject bulletPrefab; 

	// speed of bullets
	public float bulletSpeed = 10.0f;

	// speed of the character
	public float speed = 6.0f;

	// gravity applied on the character
	public float gravity = -9.8f;

	// the character controller
	private CharacterController _charController;

	// Use this for initialization
	void Start () {

		// makes the cursor invisible and locked in game screen
		Cursor.lockState = CursorLockMode.Locked;

		// initialise the character controller
		_charController = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {

		// the movement part of the script

		// Read horizontal and vertical movements into a vector and apply character speed
		Vector3 movement = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"))*speed;

		// clamp diagonal movement so that you can't move faster than speed variable
		movement = Vector3.ClampMagnitude (movement, speed);

		// apply gravity
		movement.y = gravity;

		// make the movement independent on computer
		movement *= Time.deltaTime;

		// transforms movement from local space to world space
		movement = transform.TransformDirection (movement);

		// send movement vector to the character controller to make the character move
		_charController.Move (movement);

		// escape to make the cursor visible so it's possible to "drag it outside" of the game window
		if (Input.GetKeyDown ("escape"))
			Cursor.lockState = CursorLockMode.None;

		// script for what happens when firing
		if (Input.GetButtonDown ("Fire1")) {

			DelegatesAndEvents.ShotFired(1);

			// create an instance of a bullet at bullet emitter position and at the same rotation as the bullet emitter
			GameObject temporaryBulletHandler;
			temporaryBulletHandler = Instantiate (bulletPrefab, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

			// change the velocity so the bullet moves forward with bulletSpeed
			temporaryBulletHandler.GetComponent<Rigidbody> ().velocity = temporaryBulletHandler.transform.TransformDirection(Vector3.forward*bulletSpeed);

			// make sure that bullets are destroyed after a while if the bullet doesn't hit anything
			Destroy (temporaryBulletHandler, 10.0f);
		}

	}
}
