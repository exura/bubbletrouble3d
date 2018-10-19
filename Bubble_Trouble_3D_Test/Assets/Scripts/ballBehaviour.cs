using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ballBehaviour : MonoBehaviour {

	private int level = 1;

	private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {

		_rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Push(Vector3 push) {
		_rigidbody.AddForce (push);
	}

	public void Explode() {
		Debug.Log ("Ball will explode!");
	}

	public int Level {
		get {
			return level;
		}
		set {
			if ((value > 0) && (value < 10)) {
				level = value;
			}
		}
	}
}
