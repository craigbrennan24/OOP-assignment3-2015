using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenuController : MonoBehaviour {

	public Image optionsMenu;
	public Button quitButton;
	public Button restartButton;
	public Button optionsMenuButton;
	public Text quitButtonText;
	public Text restartButtonText;
	public static bool _enabled = true;
	bool _showButtons;
	
	// Use this for initialization
	void Start () {
		optionsMenu = optionsMenu.GetComponent<Image> ();
		optionsMenuButton = optionsMenuButton.GetComponent<Button> ();
		quitButton = quitButton.GetComponent<Button> ();
		restartButton = restartButton.GetComponent<Button> ();
		quitButtonText = quitButtonText.GetComponent<Text> ();
		restartButtonText = restartButtonText.GetComponent<Text> ();
		_showButtons = false;
		Hide ();
	}

	void ShowButtons()
	{
		quitButton.enabled = true;
		quitButtonText.enabled = true;
		restartButton.enabled = true;
		restartButtonText.enabled = true;
		_showButtons = true;
	}

	void HideButtons()
	{
		quitButton.enabled = false;
		quitButtonText.enabled = false;
		restartButton.enabled = false;
		restartButtonText.enabled = false;
		_showButtons = false;
	}

	public void ToggleButtons()
	{
		if (_showButtons)
			HideButtons ();
		else
			ShowButtons ();
	}

	public void restart()
	{
		GameController.accessGameController ().restart ();
	}
	
	public void Show()
	{
		optionsMenu.enabled = true;
		optionsMenuButton.enabled = true;
		_enabled = true;
	}
	
	public void Hide()
	{
		optionsMenu.enabled = false;
		optionsMenuButton.enabled = false;
		HideButtons ();
		_enabled = false;
	}
}
