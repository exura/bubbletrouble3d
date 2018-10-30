using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// holds reference to the ballprefab
	[SerializeField] private GameObject ballPrefab;

	[SerializeField] private GameObject ballBonusPrefab;

	// holds reference to the floor
	[SerializeField] private GameObject floor;

	//Reference to player.
	[SerializeField] private GameObject player;

	// Hold multiplier.
	[SerializeField] private int bonusMultiplier = 3; // Multiplying level with this value if bonus ball.

	// reference to the score UI-element
	public Text scoreText;

	// reference to the win UI-element
	public Text winText;

	public Text gameOverText;

	// how high the balls should spawn with respect to the floor
	public float ballSpawnHeight = 10;

	// how the balls will shrink; scale/modifier, so a factor of 2 will half the size of the balls
	public float ballSizeModifier = 2.0f;

	// minimumsize allowed of the ball -> it will stop spawning balls below this size
	public float ballSizeMin = 1.0f;

	// holds score
	private int score;

	// holds how many balls left -> once it reaches zero the game is won
	private int ballsLeft;

	private int rngBonus; // Holds reference to RNG

	public int bonusPercentage = 2; // Threshold for bonus ball (probability that a ball becomes a bonus ball)

	// Use this for initialization
	void Start () {

		// start the game with a score of zero
		score = 0;

		// display the score text (we havent earned any score yet, so send 0
		SetText (0);

		// initialise how many balls left -> we start with zero balls
		ballsLeft = 0;

		// spawn the first ball
		GameObject firstBall = Instantiate (ballPrefab, new Vector3 (floor.transform.position.x, floor.transform.position.y + ballSpawnHeight, floor.transform.position.z), floor.transform.rotation) as GameObject;

		//and update the balltracker that we have one ball now -- if this is forgotten we win immediately
		ballsLeft++;

		// Tell gamecontroller to subscribe for the event that a ball is spawned
		// IMPORTANT: YOU ALSO NEED THE SAME IN AN ONDISABLE-FUNCTION OTHERWISE YOU WILL END UP WITH MEMORY LEAKS!!
		DelegatesAndEvents.onBallDestroyed += BallIsDestroyed; // + sign means we subscribe to the event, BallIsDestroyed is the method this class has implemented to handle the event onBallDestroyed
		DelegatesAndEvents.hitPlayer += playerIsHit; //Subscription to hitPlayer
	
	}
		
	// This removes the subscribing if gamecontroller is disabled or inactive, to prevent memoryleaks
	void OnDisable() {
		DelegatesAndEvents.onBallDestroyed -= BallIsDestroyed; // - sign means we unsubscribe to the event
		DelegatesAndEvents.hitPlayer -= playerIsHit;
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
		// We want to take the position, velocity, scale and level of the ball that was just destroyed
		Vector3 tempPos = ball.transform.position;
		Vector3 tempVel = ball.GetComponent<Rigidbody> ().velocity;
		Vector3 tempScale = ball.transform.localScale;
		int tempLevel = ball.GetComponent<ballBehaviour> ().Level;
		print ("Ball DESTROYED LEVEL" + tempLevel);

		// then destroy the ball so we don't accidentally spawn the new balls inside the ball being destroyed
		Destroy (ball);

		// countdown the number of balls alive
		ballsLeft--;

		// send in the points equal to the level of the ball
		SetText (tempLevel);

		// spawn new balls
		Spawn (tempPos, tempVel, tempScale, tempLevel);

		// if no new balls are spawned, we have won the game
		if (ballsLeft == 0) {
			SetWinText ();
		}
	}


	// Method for when player is hit, subscribed to event hitPlayer
	void playerIsHit(GameObject player) {
		SetGameOverText ();
		Time.timeScale = 0f; //Freezes game.

	}

	// Spawn method handles spawning of new balls
	void Spawn(Vector3 pos, Vector3 vel, Vector3 sc, int lvl) {




		// If the newly spawned ball is smaller than the minimum size allowed, it will skip the ballspawning
		if (sc.x / ballSizeModifier >= ballSizeMin) {


			// Transform into bonus ball if threshold is reached.
			rngBonus = Random.Range (1, 100);

			GameObject tmpBall;
			GameObject tmpBall2;

			// spawn the first ball
			if (rngBonus <= 100 - bonusPercentage) {
				tmpBall = Instantiate (ballPrefab, (pos) + Vector3.right * 3, floor.transform.rotation) as GameObject;
				// assign it a level higher
				tmpBall.GetComponent<ballBehaviour> ().Level = lvl + 1;
			} else {
				tmpBall = Instantiate (ballBonusPrefab, (pos) + Vector3.right * 3, floor.transform.rotation) as GameObject;
				int multLvl = tmpBall.GetComponent<ballBehaviour> ().Level * bonusMultiplier;
				tmpBall.GetComponent<ballBehaviour> ().Level = multLvl;
			}


			 // If threshold is set to 2% in inspector, RNG must give over 98 in value to create a bonus ball.
				//tmpBall.GetComponent<ballBehaviour> ().bonusBall ();
				//int multLvl = tmpBall.GetComponent<ballBehaviour> ().Level * bonusMultiplier;
				//print ("ball1 multLvl = " + multLvl); // Seems correct
				
				//print ("ball1 LEVEL = " + tmpBall.GetComponent<ballBehaviour> ().Level); // Seems correct


			// scale it with modifier
			tmpBall.transform.localScale = sc / ballSizeModifier;
			// give it the original balls velocity
			tmpBall.GetComponent<Rigidbody> ().velocity = vel;
			// then push it to the side
			tmpBall.GetComponent<ballBehaviour> ().Push (Vector3.right * 500 + Vector3.up * 2000);

			// spawn the second ball

			rngBonus = Random.Range (1, 100);
			if (rngBonus <= 100 - bonusPercentage) {
				tmpBall2 = Instantiate (ballPrefab, (pos) + Vector3.right * 3, floor.transform.rotation) as GameObject;
				// assign it a level higher
				tmpBall2.GetComponent<ballBehaviour> ().Level = lvl + 1;
			} else {
				tmpBall2 = Instantiate (ballBonusPrefab, (pos) + Vector3.right * 3, floor.transform.rotation) as GameObject;
				int multLvl = tmpBall.GetComponent<ballBehaviour> ().Level * bonusMultiplier;
				tmpBall2.GetComponent<ballBehaviour> ().Level = multLvl;
			}

			// Check for bonus and if bonusball multiply the points.

			print ("RNG: " + rngBonus);
			//print (rngBonus);

//			if (rngBonus >= (100-bonusPercentage)) { // If threshold is set to 2% in inspector, RNG must give over 98 in value to create a bonus ball.
//				print("Entered" + (100-bonusPercentage));
//				tmpBall2.GetComponent<ballBehaviour> ().bonusBall ();
//				int multLvl = tmpBall2.GetComponent<ballBehaviour> ().Level * bonusMultiplier;
//				//print ("ball2 multLvl = " + multLvl); 
//				tmpBall2.GetComponent<ballBehaviour> ().Level = multLvl;
//				//print ("ball2 LEVEL = " + tmpBall2.GetComponent<ballBehaviour> ().Level);
//			}
//

			// scale it with modifier
			tmpBall2.transform.localScale = sc / ballSizeModifier;
			// give it the original balls velocity
			tmpBall2.GetComponent<Rigidbody> ().velocity = vel;
			// then push it to the other side
			tmpBall2.GetComponent<ballBehaviour> ().Push (Vector3.right * -500 + Vector3.up * 2000);

			// add two new balls to the count of balls
			ballsLeft = ballsLeft + 2;
		}
	}
}
