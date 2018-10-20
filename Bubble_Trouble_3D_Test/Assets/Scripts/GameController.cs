using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject ballPrefab;
	[SerializeField] private GameObject floor;

	private float _floorX;
	private float _floorZ;

	public Text scoreText;
	private int score;
	private List<GameObject> balls;

	public float ballSpawnHeight = 10;

	// Use this for initialization
	void Start () {

		balls = new List<GameObject> ();

		_floorX = floor.transform.position.x;
		_floorZ = floor.transform.position.z;

		Debug.Log("FLOOR HERE: " + floor.transform.position.ToString ());

		score = 0;

		SetText ();

		Debug.Log ("Size of list: " + balls.Count.ToString ());

		GameObject tempBall = Instantiate (ballPrefab, new Vector3 (_floorX, ballSpawnHeight, _floorZ), floor.transform.rotation) as GameObject;

		balls.Add(tempBall);

		Debug.Log ("Size of list: " + balls.Count.ToString ());
	}
	
	// Update is called once per frame
	void Update () {

		/*foreach (GameObject ball in balls) {

			ballBehaviour behav = ball.GetComponent<ballBehaviour> ();

			if (ball != null) {
				if (!behav.Active) {
				}

			}
		}*/

		for (int i = balls.Count - 1; i>=0; i--) {

			if (!balls [i].GetComponent<ballBehaviour> ().Active) {
				Vector3 tempPos = balls [i].transform.position;
				Destroy (balls [i]);
				balls.RemoveAt (i);
				Spawn (tempPos);
			}
		}
		
	}

	void SetText() {

		scoreText.text = "Score: " + score.ToString ();

	}

	void Spawn(Vector3 pos) {
		balls.Add(Instantiate (ballPrefab, (pos) + Vector3.right*3, floor.transform.rotation) as GameObject);
		balls.Add(Instantiate (ballPrefab, (pos) + Vector3.right*-3, floor.transform.rotation) as GameObject);
	}
}
