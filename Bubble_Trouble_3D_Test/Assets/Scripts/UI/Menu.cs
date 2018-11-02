using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Canvas MainCanvas;
	//Public Canvas HighscoresCanvas; /When done, just duplicate main canvas and change to fit.
	//Public Canvas CreditsCanvas; /When done, just duplicate main canvas and change to fit.

	void Awake()
	{
		Cursor.lockState = CursorLockMode.None;
		//HighscoresCanvas.enabled = false;
		//CreditsCanvas.enabled = false;
	}

	public void LoadOn()
	{
		Application.LoadLevel (1);
	}


}
