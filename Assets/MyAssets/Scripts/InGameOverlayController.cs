using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameOverlayController : MonoBehaviour {

	public Canvas scoreboard;
	public Text scoreText;
	public Button pauseButton;
	public static bool _enabled = true;
	int lastCheck;
	bool checkShapeList = true;
	
	// Use this for initialization
	void Start () {
		scoreboard = scoreboard.GetComponent<Canvas> ();
		scoreText = scoreText.GetComponent<Text> ();
		pauseButton = pauseButton.GetComponent<Button> ();
		scoreText.text = "0\nscore";
		lastCheck = 0;
	}

	public void resetCounter()
	{
		lastCheck = 0;
	}

	void checkForNewShape()
	{
		if (GameController.accessGameController ().finishedShapes.Count > lastCheck) {

		}
	}
	
	public void Show()
	{
		scoreboard.enabled = true;
		_enabled = true;
	}
	
	public void Hide()
	{
		scoreboard.enabled = false;
		_enabled = false;
	}

}
