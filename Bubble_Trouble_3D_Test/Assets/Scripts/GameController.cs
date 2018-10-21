using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject ballPrefab;
	[SerializeField] private GameObject floor;

	private float _floorX;
	private float _floorZ;
	private Vector3 scale = new Vector3(4f, 4f, 4f);

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
				Vector3 tempVel = balls [i].GetComponent<Rigidbody>().velocity;


				Destroy (balls [i]);
				balls.RemoveAt (i);
				Spawn (tempPos, tempVel);
			}
		}
		
	}

	void SetText() {

		scoreText.text = "Score: " + score.ToString ();

	}

	void Spawn(Vector3 pos, Vector3 vel) {
		print (scale);
		scale = scale / 2.0f;
		GameObject tmpBall = Instantiate (ballPrefab, (pos) + Vector3.right * 3, floor.transform.rotation) as GameObject;
		balls.Add(tmpBall);
		tmpBall.GetComponent<Rigidbody> ().velocity = vel;
		tmpBall.GetComponent<ballBehaviour> ().Push (Vector3.right*1000+Vector3.up*5000);
		tmpBall.transform.localScale = scale;
		print (tmpBall.transform.localScale);

		GameObject tmpBall2 = Instantiate (ballPrefab, (pos) + Vector3.right * -3, floor.transform.rotation) as GameObject;
		//(tmpBall.GetComponent<ballBehaviour> ()).Push (Vector3.right * -300);
		balls.Add(tmpBall2);
		tmpBall2.GetComponent<Rigidbody> ().velocity = vel;
		tmpBall2.GetComponent<ballBehaviour> ().Push (Vector3.right*-1000+Vector3.up*5000);
		tmpBall2.transform.localScale = scale;
		print (tmpBall2.transform.localScale);


	}
}
