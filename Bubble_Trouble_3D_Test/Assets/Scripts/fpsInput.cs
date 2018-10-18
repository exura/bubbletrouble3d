﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]


public class fpsInput : MonoBehaviour {

	[SerializeField] private GameObject bulletPrefab;
	private GameObject _bullet;

	public float speed = 6.0f;
	public float gravity = -9.8f;

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
		movement.y = gravity;

		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_charController.Move (movement);

		if (Input.GetKeyDown ("escape"))
			Cursor.lockState = CursorLockMode.None; //enable mouse /GETAWAY!!

		if (Input.GetButtonDown ("Fire1")) {
			_bullet = Instantiate (bulletPrefab) as GameObject;
			_bullet.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
			_bullet.transform.rotation = transform.rotation;
		}

	}
}
