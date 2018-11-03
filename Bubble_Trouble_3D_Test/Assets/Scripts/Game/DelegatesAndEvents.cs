using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegatesAndEvents : MonoBehaviour {

	// source: https://www.youtube.com/watch?v=ihIOVj9t0_E

	//1. Define delegate and events

	// first define the delegate (a container for a function)
	public delegate void BallEventHandler (GameObject ball);
	public delegate void PlayerEventHandler (GameObject player); 
	public delegate void ShotFiredEvent (int nbr); 

	// then the event
	public static event BallEventHandler onBallDestroyed; // event to handle when ball is destroyed

	public static event PlayerEventHandler hitPlayer; // Event to handle when ball hits a player

	public static event ShotFiredEvent shotFired;


	//2. trigger the events
	public static void BallDestroyed(GameObject ball)
	{
		if (onBallDestroyed != null) {
			onBallDestroyed (ball);
		}
	}

	public static void ballHitPlayer(GameObject player)
	{
		if (hitPlayer != null) {
			hitPlayer (player);
		}
	}

	public static void ShotFired(int nbr)
	{
		if (shotFired != null) {
			shotFired (nbr);
		}
	}


	//3. subscribing gameobject to the events
	// you should tell your gameobject to listen for this
	// in this example we will tell GameController to listen for this
}
