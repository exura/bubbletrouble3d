using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// holds reference to the ballprefab
	[SerializeField] private GameObject ballPrefab;

	[SerializeField] private GameObject ballBonusPrefab;

	[SerializeField] private GameObject pickUpPrefab;


	// holds reference to the explosion
	[SerializeField] private GameObject explo;

	// holds reference to the floor
	[SerializeField] private GameObject floor;

	// hold reference to one wall
	[SerializeField] private GameObject wall;

	//Reference to player.
	[SerializeField] private GameObject player;

	// Hold multiplier.
	[SerializeField] private int bonusMultiplier = 3; // Multiplying level with this value if bonus ball.

	// reference to the score UI-element
	public Text scoreText;

	// reference to the win UI-element
	public Text winText;

	public Text gameOverText;

	public Text shotsFiredText;

	public Text accuracyText;

	public Text rankText;

	// how high the balls should spawn with respect to the floor
	public float ballSpawnHeight = 10;

	// how the balls will shrink; scale/modifier, so a factor of 2 will half the size of the balls
	public float ballSizeModifier = 2.0f;

	// minimumsize allowed of the ball -> it will stop spawning balls below this size
	public float ballSizeMin = 1.0f;

	// how fast the velocity of a spawned ball can be at max
	public float maxRandomBallSpeed = 6.0f;

	// how many balls per spawn
	public int nbrOfBallsPerSpawn = 2;

	// holds score
	private int score;
	private int nbrShotsFired;
	private float accuracyPct;
	private int nbrOfBallsHit;

	// holds how many balls left -> once it reaches zero the game is won
	private int ballsLeft;

	private int rngBonus; // Holds reference to RNG

	public int bonusPercentage = 20; // Threshold for bonus ball (probability that a ball becomes a bonus ball)

	public int pickupPercentage = 20;

	// Use this for initialization
	void Start () {

		// start the game with a score of zero
		score = 0;
		nbrShotsFired = 0;
		accuracyPct = 0.0f;
		nbrOfBallsHit = 0;

		// display the score text (we havent earned any score yet, so send 0
		SetText (0);

		// initialise how many balls left -> we start with zero balls
		ballsLeft = 0;

		// spawn the first ball
		GameObject firstBall = Instantiate (ballPrefab, new Vector3 (floor.transform.position.x, floor.transform.position.y + ballSpawnHeight, floor.transform.position.z), Random.rotation) as GameObject;

		//and update the balltracker that we have one ball now -- if this is forgotten we win immediately
		ballsLeft++;

		// Tell gamecontroller to subscribe for the event that a ball is spawned
		// IMPORTANT: YOU ALSO NEED THE SAME IN AN ONDISABLE-FUNCTION OTHERWISE YOU WILL END UP WITH MEMORY LEAKS!!
		DelegatesAndEvents.onBallDestroyed += BallIsDestroyed; // + sign means we subscribe to the event, BallIsDestroyed is the method this class has implemented to handle the event onBallDestroyed
		DelegatesAndEvents.hitPlayer += playerIsHit; //Subscription to hitPlayer
		DelegatesAndEvents.shotFired += ShotIsFired; //Subscription when shot is fired
	
	}
		
	// This removes the subscribing if gamecontroller is disabled or inactive, to prevent memoryleaks
	void OnDisable() {
		DelegatesAndEvents.onBallDestroyed -= BallIsDestroyed; // - sign means we unsubscribe to the event
		DelegatesAndEvents.hitPlayer -= playerIsHit;
		DelegatesAndEvents.shotFired -= ShotIsFired; 
	}

	// Set the score-text given that we are adding points
	void SetText(int points) {

		// Add the number of points to the score
		score = score + points;

		// Update the text
		scoreText.text = "Score: " + score.ToString ();

	}

	// In case the player wins, the wintext is displayed
	void SetWinText() {
		winText.enabled = true;
	}

	// In case the player is hit by a ball, the game over text is displayed
	void SetGameOverText() {
		gameOverText.enabled = true;
	}

	// this is the method implemented to handle the event onBallDestroyed
	void BallIsDestroyed(GameObject ball) {
		nbrOfBallsHit++;
		SetStatisticsText ();
		// We want to take the position, velocity, scale and level of the ball that was just destroyed
		Vector3 tempPos = ball.transform.position;
		Vector3 tempVel = ball.GetComponent<Rigidbody> ().velocity;
		Vector3 tempScale = ball.transform.localScale;
		int tempLevel = ball.GetComponent<ballBehaviour> ().Level;
		int tempBonus = ball.GetComponent<ballBehaviour> ().Bonus;

		// then destroy the ball so we don't accidentally spawn the new balls inside the ball being destroyed
		Destroy (ball);

		GameObject explosion = Instantiate (explo,tempPos, floor.transform.rotation) as GameObject;

		// countdown the number of balls alive
		ballsLeft--;


		// send in the points equal to the level of the ball
		SetText (tempBonus);

		// spawn new balls
		Spawn (tempPos, tempVel, tempScale, tempLevel);

		SpawnPickup (tempPos);

		Destroy (explosion, 5.0f);

		// if no new balls are spawned, we have won the game
		if (ballsLeft == 0) {
			SetWinText ();
			Time.timeScale = 0f; //Freezes game.
		}
	}


	// Method for when player is hit, subscribed to event hitPlayer
	void playerIsHit(GameObject player) {
//		SetGameOverText ();
//		Time.timeScale = 0f; //Freezes game.
		PlayerPrefs.SetInt("Player Score", score); // Saves score to be used across scenes.
		Application.LoadLevel (2);

	}

	void ShotIsFired(int nbr) {
		nbrShotsFired = nbrShotsFired + nbr;
		SetStatisticsText ();
	}

	void SetStatisticsText() {
		if(nbrShotsFired>0) {
			accuracyPct = (float)nbrOfBallsHit / (float)nbrShotsFired*100;
			print ("nbr of balls hit: " + nbrOfBallsHit);
			print ("nbr of shots fired: " + nbrShotsFired);
			print ("accuracy pct: " + accuracyPct);

			shotsFiredText.text = "Number of shots fired: " + nbrShotsFired.ToString ();
			accuracyText.text = "Accuracy: " + accuracyPct.ToString ("F2") + "%";

			shotsFiredText.enabled = true;
			accuracyText.enabled = true;
			SetRankText ();
		}
	}

	void SetRankText() {
		rankText.enabled = true;

		if (nbrShotsFired == 0) {
			rankText.text = "Need help to find the fire button?";
		} else if (nbrShotsFired <= 10 && nbrOfBallsHit == 0) {
			rankText.text = "Aim at the balls!";
		} else if (nbrShotsFired <= 20 && nbrOfBallsHit == 0) {
			rankText.text = "Keep trying!";
		} else if (nbrShotsFired <= 50 && nbrOfBallsHit == 0) {
			rankText.text = "You need glasses?";
		} else if (nbrShotsFired <= 100 && nbrOfBallsHit == 0) {
			rankText.text = "I'm not calling you n00b ... yet...";
		} else if (nbrShotsFired <= 200 && nbrOfBallsHit == 0) {
			rankText.text = "Maybe you should try another game?";
		} else if (nbrShotsFired > 200 && nbrOfBallsHit == 0) {
			rankText.text = "Have you heard of a game called Diablo?";
		} else if (accuracyPct >= 100.0f) {
			rankText.text = "Your aim is Godlike!";
		} else if (accuracyPct >= 80.0f) {
			rankText.text = "Your aim is sharp!";
		} else if (accuracyPct >= 20.0f) {
			rankText.text = "Your aim is ok!";
		} else if (accuracyPct >= 10.0f) {
			rankText.text = "Who said this was easy?";
		} else if (accuracyPct >= 5.0f) {
			rankText.text = "If you get closer to the ball you might hit it!";
		} else if (accuracyPct >= 0.0f) {
			rankText.text = "The balls get smaller and so does your LUCK!";
		} else {
			rankText.text = "This was not supposed to happen!";
		}

		
	}

	// Spawn method handles spawning of new balls
	void Spawn(Vector3 pos, Vector3 vel, Vector3 sc, int lvl) {

		// If the newly spawned ball is smaller than the minimum size allowed, it will skip the ballspawning
		if (sc.x / ballSizeModifier >= ballSizeMin && sc.y / ballSizeModifier >= ballSizeMin && sc.z / ballSizeModifier >= ballSizeMin) {

			Vector3 invertVel = Vector3.zero;

			for (int i = 0; i < nbrOfBallsPerSpawn; i++) {

				// Transform into bonus ball if threshold is reached.
				int rngBonus = Random.Range (1,100);

				GameObject tmpBall;

				int xRand;
				int zRand;

				if (Random.Range (1, 100) <= 50) {
					xRand = -1;
				} else {
					xRand = 1;
				}

				if (Random.Range (1, 100) <= 50) {
					zRand = -1;
				} else {
					zRand = 1;
				}

				Vector3 tmpPos = new Vector3(xRand*Random.Range(sc.x / ballSizeModifier,sc.x / ballSizeModifier * 2.0f),Random.Range(sc.x,sc.x * 2.0f),zRand*Random.Range(sc.x / ballSizeModifier,sc.x / ballSizeModifier * 2.0f));

				// If new random position is outside the room, then invert the random position so it's inside the room
				if (tmpPos.x + pos.x > floor.transform.position.x + floor.transform.localScale.x / 2) {
					print ("Xpos too high: " + (tmpPos.x+pos.x));
					tmpPos.x -= Mathf.Abs(tmpPos.x);
					print ("New xpos: " + (tmpPos.x+pos.x));
				} else if (tmpPos.x + pos.x < floor.transform.position.x - floor.transform.localScale.x / 2) {
					print ("Xpos too low: " + (tmpPos.x+pos.x));
					tmpPos.x += Mathf.Abs(tmpPos.x);
					print ("New xpos: " + (tmpPos.x+pos.x));
				}

				// If new random position is outside the room, then invert the random position so it's inside the room
				if (tmpPos.z + pos.z > floor.transform.position.z + floor.transform.localScale.z / 2) {
					print ("zpos too high: " + (tmpPos.z+pos.z));
					tmpPos.z -= Mathf.Abs(tmpPos.z);
					print ("New zpos: " + (tmpPos.z+pos.z));
				} else if (tmpPos.z + pos.z < floor.transform.position.z - floor.transform.localScale.z / 2) {
					print ("zpos too low: " + (tmpPos.z+pos.z));
					tmpPos.z += Mathf.Abs(tmpPos.z);
					print ("New zpos: " + (tmpPos.z+pos.z));
				}

				// If new random position is outside the room, then invert the random position so it's inside the room
				if (tmpPos.y + pos.y > wall.transform.position.y + wall.transform.localScale.x / 2) {
					print ("ypos too high: " + (tmpPos.y+pos.y));
					tmpPos.y = wall.transform.position.y + wall.transform.localScale.y / 2 - sc.y / ballSizeModifier;
					print ("New ypos " + (tmpPos.y+pos.y));
				} else if (tmpPos.y + pos.y < floor.transform.position.y + floor.transform.localScale.y / 2) {
					print ("ypos too low: " + (tmpPos.y+pos.y));
					tmpPos.y += tmpPos.y;
					print ("New ypos " + (tmpPos.y+pos.y));
				}

				// spawn the first ball
				if (rngBonus <= bonusPercentage) {
					tmpBall = Instantiate (ballBonusPrefab, (pos) + tmpPos, Random.rotation) as GameObject;
					// assign it a level higher
					tmpBall.GetComponent<ballBehaviour> ().Level = lvl + 1;
					tmpBall.GetComponent<ballBehaviour> ().Bonus = lvl + 1;
				} else {
					tmpBall = Instantiate (ballPrefab, (pos) + tmpPos, Random.rotation) as GameObject;
					int multLvl = (tmpBall.GetComponent<ballBehaviour> ().Level + 1) * bonusMultiplier;
					tmpBall.GetComponent<ballBehaviour> ().Level = lvl + 1;
					tmpBall.GetComponent<ballBehaviour> ().Bonus = multLvl;
				}

				// scale it with modifier
				tmpBall.transform.localScale = sc / ballSizeModifier;
				// then push it to the side
				tmpBall.GetComponent<ballBehaviour> ().GiveVelocity(vel,maxRandomBallSpeed,true);

				if (i == 0) {
					invertVel = tmpBall.GetComponent<ballBehaviour> ().GetVelocity ();
				} else {
					tmpBall.GetComponent<ballBehaviour> ().SetVelocity (new Vector3(-invertVel.x,invertVel.y,-invertVel.z));
				}

			}

			// add nbrOfBallsPerSpawn new balls to the count of balls
			ballsLeft = ballsLeft + nbrOfBallsPerSpawn;
		}
	}

	void SpawnPickup(Vector3 pos) {
		int rng = Random.Range (1, 100);

		if (rng <= pickupPercentage) {
			GameObject somePickup = Instantiate (pickUpPrefab, pos, floor.transform.rotation);
		}
	}
}
