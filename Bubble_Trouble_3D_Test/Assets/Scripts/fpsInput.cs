using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]


public class fpsInput : MonoBehaviour {

	[SerializeField] private GameObject bulletEmitter;
	[SerializeField] private GameObject bulletPrefab;
	public float bulletForce = 50;

	public float speed = 6.0f;
	public float gravity = -9.8f;
	public Camera cam;

	private CharacterController _charController;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		_charController = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {

		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude (movement, speed);
		//movement.y = gravity; //Commenting out for now /S

		movement *= Time.deltaTime;
		movement = cam.transform.TransformDirection (movement); //Always use CAMERA as direction of movement
		_charController.Move (movement);

		if (Input.GetKeyDown ("escape"))
			Cursor.lockState = CursorLockMode.None; //enable mouse /GETAWAY!!

		if (Input.GetButtonDown ("Fire1")) {

			GameObject temporaryBulletHandler;
			temporaryBulletHandler = Instantiate (bulletPrefab, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

			Rigidbody temporaryRigidBody;
			temporaryRigidBody = temporaryBulletHandler.GetComponent<Rigidbody> ();

			Vector3 temporaryVector;
			temporaryVector = _charController.velocity;

			temporaryRigidBody.AddForce (temporaryVector + transform.forward * bulletForce);

			Destroy (temporaryBulletHandler, 10.0f);
		}

	}
}
