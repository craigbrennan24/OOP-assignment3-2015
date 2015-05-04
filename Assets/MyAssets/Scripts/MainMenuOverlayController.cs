using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuOverlayController : MonoBehaviour {

	public Button startButton;
	public Button backButton;
	public Button instructionsButton;
	public Button aboutButton;
	public Button exitButton;
	public Text titleText;
	public Text instructionsScreenText;

	public Text startButtonText;
	public Text backButtonText;
	public Text instructionsButtonText;
	public Text aboutButtonText;
	public Text exitButtonText;

	//About Screen
	public Text aboutScreenText;
	public Text aboutScreenTitle;
	public Image aboutScreenImage;
	public Text aboutScreenCredit;

	// Use this for initialization
	void Start () {
		titleText = titleText.GetComponent<Text> ();
		startButton = startButton.GetComponent<Button> ();
		backButton = backButton.GetComponent<Button> ();
		instructionsButton = instructionsButton.GetComponent<Button> ();
		aboutButton = aboutButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
		startButtonText = startButtonText.GetComponent<Text> ();
		backButtonText = backButtonText.GetComponent<Text> ();
		instructionsButtonText = instructionsButtonText.GetComponent<Text> ();
		aboutButtonText = aboutButtonText.GetComponent<Text> ();
		exitButtonText = exitButtonText.GetComponent<Text> ();
		instructionsScreenText = instructionsScreenText.GetComponent<Text> ();

		//Initialize about screen
		aboutScreenText = aboutScreenText.GetComponent<Text> ();
		aboutScreenTitle = aboutScreenTitle.GetComponent<Text> ();
		aboutScreenImage = aboutScreenImage.GetComponent<Image> ();
		aboutScreenCredit = aboutScreenCredit.GetComponent<Text> ();

		showMenu ();
	}

	public void launchGame() {
		Application.LoadLevel ("Main");
	}

	public void showMenu()
	{
		changeAboutScreenState (false);
		changeInstructionsScreenState (false);
		changeMenuState (true);
	}

	public void showAboutScreen()
	{
		changeInstructionsScreenState (false);
		changeMenuState (false);
		changeAboutScreenState (true);
	}

	void changeInstructionsScreenState( bool state )
	{
		changeBackButtonState (state);
	}

	void changeAboutScreenState( bool state )
	{
		changeBackButtonState (state);

		aboutScreenCredit.enabled = state;
		aboutScreenText.enabled = state;
		aboutScreenImage.enabled = state;
		aboutScreenTitle.enabled = state;
	}

	void changeBackButtonState( bool state )
	{
		backButton.enabled = state;
		backButtonText.enabled = state;
	}

	void changeMenuState( bool state )
	{
		titleText.enabled = state;
		startButton.enabled = state;
		startButtonText.enabled = state;
		instructionsButton.enabled = state;
		instructionsButtonText.enabled = state;
		aboutButton.enabled = state;
		aboutButtonText.enabled = state;
		exitButton.enabled = state;
		exitButtonText.enabled = state;
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
