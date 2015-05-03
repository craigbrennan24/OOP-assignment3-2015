using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenuController : MonoBehaviour {

	public Image gameOverMenu;
	public Text gameOverMenuText;
	public Button quitButton;
	public Button restartButton;
	public Text quitButtonText;
	public Text restartButtonText;
	public static bool _enabled = true;

	// Use this for initialization
	void Start () {
		gameOverMenu = gameOverMenu.GetComponent<Image> ();
		gameOverMenuText = gameOverMenuText.GetComponent<Text> ();
		quitButton = quitButton.GetComponent<Button> ();
		quitButtonText = quitButtonText.GetComponent<Text> ();
		restartButtonText = restartButtonText.GetComponent<Text> ();
		restartButton = restartButton.GetComponent<Button> ();
		Hide ();
	}

	public void ExitGame()
	{
		Application.LoadLevel ("Launch");
	}

	public void Restart()
	{
		GameController.accessGameController ().restart ();
	}

	public void Show()
	{
		gameOverMenu.enabled = true;
		gameOverMenuText.enabled = true;
		quitButton.enabled = true;
		quitButtonText.enabled = true;
		restartButton.enabled = true;
		restartButtonText.enabled = true;
		_enabled = true;
	}

	public void Hide()
	{
		gameOverMenu.enabled = false;
		gameOverMenuText.enabled = false;
		quitButton.enabled = false;
		quitButtonText.enabled = false;
		restartButton.enabled = false;
		restartButtonText.enabled = false;
		_enabled = false;
	}
}
