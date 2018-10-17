using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterController : MonoBehaviour {

	public float speed = 10.0f;

	public Text scoreText;

	private int score;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;

		score = 0;

		SetText ();
	}
	
	// Update is called once per frame
	void Update () {

		float translation = Input.GetAxis ("Vertical") * speed;
		float strafe = Input.GetAxis ("Horizontal") * speed;
		translation *= Time.deltaTime;
		strafe *= Time.deltaTime;

		transform.Translate (strafe, 0, translation);

		if (Input.GetKeyDown ("escape"))
			Cursor.lockState = CursorLockMode.None; //enable mouse /GETAWAY!!

	}

	void SetText() {

		scoreText.text = "Score: " + score.ToString ();

	}
}
