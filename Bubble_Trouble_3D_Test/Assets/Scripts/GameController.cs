using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject ballPrefab;
	[SerializeField] private GameObject floor;

	public Text scoreText;
	public Text winText;
	public float ballSpawnHeight = 10;
	public float ballSizeModifier = 2.0f;
	public float ballSizeMin = 1.0f;

	private float _floorX;
	private float _floorZ;
	private int score;
	private List<GameObject> balls;
	private Vector3 scale;


	// Use this for initialization
	void Start () {

		balls = new List<GameObject> ();

		_floorX = floor.transform.position.x;
		_floorZ = floor.transform.position.z;

		score = 0;

		SetText (0);

		GameObject tempBall = Instantiate (ballPrefab, new Vector3 (_floorX, ballSpawnHeight, _floorZ), floor.transform.rotation) as GameObject;

		balls.Add(tempBall);
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = balls.Count - 1; i>=0; i--) {

			if (!balls [i].GetComponent<ballBehaviour> ().Active) {
				Vector3 tempPos = balls [i].transform.position;
				Vector3 tempVel = balls [i].GetComponent<Rigidbody>().velocity;
				scale = balls [i].transform.localScale;
				Destroy (balls [i]);
				balls.RemoveAt (i);
				Spawn (tempPos, tempVel, scale);
				SetText (1);
			}
		}

		if (balls.Count == 0) {
			SetWinText ();
		}
		
	}

	void SetText(int points) {

		score = score + points;

		scoreText.text = "Score: " + score.ToString ();

	}

	void SetWinText() {
		winText.enabled = true;
	}

	void Spawn(Vector3 pos, Vector3 vel, Vector3 sc) {

		if (scale.x / ballSizeModifier >= ballSizeMin) {
			GameObject tmpBall = Instantiate (ballPrefab, (pos) + Vector3.right * 3, floor.transform.rotation) as GameObject;
			tmpBall.transform.localScale = scale / ballSizeModifier;
			balls.Add (tmpBall);
			tmpBall.GetComponent<Rigidbody> ().velocity = vel;
			tmpBall.GetComponent<ballBehaviour> ().Push (Vector3.right * 500 + Vector3.up * 2000);

			GameObject tmpBall2 = Instantiate (ballPrefab, (pos) + Vector3.right * -3, floor.transform.rotation) as GameObject;
			tmpBall2.transform.localScale = scale / ballSizeModifier;
			balls.Add (tmpBall2);
			tmpBall2.GetComponent<Rigidbody> ().velocity = vel;
			tmpBall2.GetComponent<ballBehaviour> ().Push (Vector3.right * -500 + Vector3.up * 2000);
		}
	}
}
