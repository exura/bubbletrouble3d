using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour {

	// create enumeration which is easier to use in the component
	public enum RotationAxes {
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}

	// initialise the enumeration
	public RotationAxes axes = RotationAxes.MouseXAndY;

	// Sensitivity for horizontal mouse movement
	public float sensitivityHor = 9.0f;

	// Sensitivity for vertical mouse movement
	public float sensitivityVert = 9.0f;

	// minimum vertical mousemovement allowed
	public float minimumVert = -45.0f;

	// maximal vertical mousemovement allowed
	public float maximumVert = 45.0f;

	// initialize horizontal rotation
	private float _rotationX = 0;

	// Update is called once per frame
	void Update () {
		
		// horizontal rotation here -- used on player object
		if (axes == RotationAxes.MouseX) {
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

		// vertical rotation here -- used on camera
		} else if (axes == RotationAxes.MouseY) {

			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp (_rotationX, minimumVert, maximumVert);

			float rotationY = transform.localEulerAngles.y;

			transform.localEulerAngles = new Vector3 (_rotationX, rotationY, 0);
			// both horizontal and vertical rotation here
		} else {
			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp (_rotationX, minimumVert, maximumVert);

			float delta = Input.GetAxis ("Mouse X") * sensitivityHor;
			float rotationY = transform.localEulerAngles.y + delta;

			transform.localEulerAngles = new Vector3 (_rotationX, rotationY, 0);
		}
	}

}