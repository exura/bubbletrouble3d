using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gameOverMenu : MonoBehaviour {

	public Canvas gameOverScreen;



	void Awake()
	{
		Cursor.lockState = CursorLockMode.None;
		//HighscoresCanvas.enabled = false;
		//CreditsCanvas.enabled = false;
	}

	public void LoadOn()
	{
		Application.LoadLevel (0);
	}


}
