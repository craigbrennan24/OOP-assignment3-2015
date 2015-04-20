using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InGameOverlayController : MonoBehaviour {

	public Canvas overlay;
	public Text scoreText;
	public Text scoreGainText;
	public Button pauseButton;
	public static bool _enabled = true;
	Vector2 scoreGainPos_base;
	bool scoreGainDisplayed = false;
	Queue<Shape> scoreQueue;
	float lastScorePop;
	Vector3 scoreGainSpeed;
	int lastCheck;
	
	// Use this for initialization
	void Start () {
		overlay = overlay.GetComponent<Canvas> ();
		scoreText = scoreText.GetComponent<Text> ();
		scoreGainText = scoreGainText.GetComponent<Text> ();
		Vector3 temp = scoreGainText.GetComponent<Transform> ().position;
		scoreGainPos_base = new Vector2 (temp.x, temp.y);
		pauseButton = pauseButton.GetComponent<Button> ();
		scoreText.text = "0\nscore";
		scoreQueue = new Queue<Shape> ();
		lastScorePop = 0;
		scoreGainSpeed = new Vector3 (0.0f, 1.0f, 0.0f);
		lastCheck = 0;
		hideScoreGain ();
		//hideOptionsMenu ();
	}

	public void resetCounter()
	{
		lastCheck = 0;
	}

	void Update()
	{
		if (!GameController.paused) {
			checkForNewShape ();
			if (Time.time - lastScorePop > 1) {
				if (scoreQueue.Count > 0) {
					scorePopup_start (scoreQueue.Dequeue ());
				}
			}
			scoreUpdate ();
			scorePopup_update ();
		}
	}

	public void resetPauseButton()
	{
		pauseButton.GetComponent<Text>().fontSize = 30;
		pauseButton.GetComponent<Text>().text = "pause";
		pauseButton.enabled = true;
	}

	void checkForNewShape()
	{
		int newCheck = GameController.accessGameController ().finishedShapes.Count;
		if (newCheck > lastCheck) {
			int counter = lastCheck;
			lastCheck = newCheck;
			for( int i = counter; i < newCheck; i++ )
			{
				scoreQueue.Enqueue(GameController.accessGameController ().finishedShapes[i]);
			}
		}
	}

	public void reset()
	{
		resetPauseButton ();
		hideGameOverMenu ();
		hideOptionsMenu ();
		resetCounter ();
	}

	public void showOptionsMenu()
	{
		GetComponent<OptionsMenuController>().Show ();
	}

	public void hideOptionsMenu()
	{
		GetComponent<OptionsMenuController>().Hide ();
	}

	public void showGameOverMenu()
	{
		GetComponent<GameOverMenuController>().Show ();
	}

	public void hideGameOverMenu()
	{
		GetComponent<GameOverMenuController>().Hide ();
	}

	void scorePopup_start( Shape score )
	{
		showScoreGain ();
		resetGainDisplayPos ();
		scoreGainText.text = "+" + score.getValue ();
		if (score.givesScoreMultiplier ()) {
			scoreGainText.text += " x" + GameController.scoreMultiplier;
		}
		if (score.givesComboMultiplier ()) {
			float combo = GameController.scoreMultiplier_combo;
			if( combo == 1 )
				scoreGainText.text += "\nx2.5";
			else if( combo == 2 )
				scoreGainText.text += "\nx5";
			else if( combo == 3 )
				scoreGainText.text += "\nx10";
		}
		lastScorePop = Time.time;
	}

	void scoreUpdate()
	{
		scoreText.text = GameController.score + "\nscore";
		if (GameController.score > 99999) {
			scoreText.fontSize = 30;
			if( GameController.score > 999999 )
				scoreText.fontSize = 25;
		}
	}

	void scorePopup_update()
	{
		if (scoreGainDisplayed) {
			if( Time.time - lastScorePop < 1 )
			{
				scoreGainText.GetComponent<Transform>().position += scoreGainSpeed;
			} else {
				hideScoreGain();
			}
		}
	}

	void hideScoreGain()
	{
		scoreGainText.enabled = false;
		scoreGainDisplayed = false;
	}

	void showScoreGain()
	{
		scoreGainText.enabled = true;
		scoreGainDisplayed = true;
	}

	void resetGainDisplayPos()
	{
		scoreGainText.GetComponent<Transform> ().position = new Vector3 (scoreGainPos_base.x, scoreGainPos_base.y, 0.0f);
	}
	
	public void Show()
	{
		overlay.enabled = true;
		_enabled = true;
	}
	
	public void Hide()
	{
		overlay.enabled = false;
		_enabled = false;
	}

}
