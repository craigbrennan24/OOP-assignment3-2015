using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenuController : MonoBehaviour {

	public Canvas gameOverMenu;
	public Button quitButton;
	public Button restartButton;
	public static bool _enabled = true;

	// Use this for initialization
	void Start () {
		gameOverMenu = gameOverMenu.GetComponent<Canvas> ();
		quitButton = quitButton.GetComponent<Button> ();
		restartButton = restartButton.GetComponent<Button> ();
		Hide ();
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void Restart()
	{
		GameController.accessGameController ().setup ();
	}

	public void Show()
	{
		gameOverMenu.enabled = true;
		quitButton.enabled = true;
		restartButton.enabled = true;
		_enabled = true;
	}

	public void Hide()
	{
		gameOverMenu.enabled = false;
		quitButton.enabled = false;
		restartButton.enabled = false;
		_enabled = false;
	}
}
