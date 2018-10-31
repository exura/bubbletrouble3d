using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gameOverMenu : MonoBehaviour {

	public Canvas gameOverScreen;
	public Text scoreText;
	private int score; 



	void Awake()
	{
		Cursor.lockState = CursorLockMode.None;
		score = PlayerPrefs.GetInt("Player Score");
		scoreText.text = "Your Score: " + score.ToString ();
		//HighscoresCanvas.enabled = false;
		//CreditsCanvas.enabled = false;
	}

	public void LoadOn()
	{
		Application.LoadLevel (0);
	}


}
