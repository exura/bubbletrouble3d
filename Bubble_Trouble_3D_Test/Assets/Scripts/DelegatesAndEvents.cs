using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegatesAndEvents : MonoBehaviour {

	// source: https://www.youtube.com/watch?v=ihIOVj9t0_E

	//1. Define delegate and events

	// first define the delegate (a container for a function)
	public delegate void BallEventHandler (GameObject ball);

	// then the event
	public static event BallEventHandler onBallDestroyed;

	//2. trigger the events
	public static void BallDestroyed(GameObject ball)
	{
		if (onBallDestroyed != null) {
			onBallDestroyed (ball);
		}
	}

	//3. subscribing gameobject to the events
	// you should tell your gameobject to listen for this
	// in this example we will tell GameController to listen for this
}
