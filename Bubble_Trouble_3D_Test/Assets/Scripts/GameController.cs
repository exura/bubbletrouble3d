using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text scoreText;

	private int score;

	// Use this for initialization
	void Start () {
		score = 0;

		SetText ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetText() {

		scoreText.text = "Score: " + score.ToString ();

	}
}
