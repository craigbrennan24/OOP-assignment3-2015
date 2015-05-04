using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuOverlayController : MonoBehaviour {

	//Main Menu Screen
	int instructionsScreenState = 0;
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

	//Instructions Screen
	public Text instructionsTitleText;
	public Button instructionsButtonRight;
	public Image instructionsButtonRight_image;
	public Image instructionsButtonLeft_image;
	public Button instructionsButtonLeft;
	public Text instructionsButtonRight_text;
	public Text instructionsButtonLeft_text;
	public Text instructions_page1_text;
	public Text instructions_page2_text;
	public Text instructions_page3_text;
	public Image instructions_page3_image;

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

		//Initialize instructions screen
		instructionsTitleText = instructionsTitleText.GetComponent<Text> ();
		instructionsButtonRight = instructionsButtonRight.GetComponent<Button> ();
		instructionsButtonLeft = instructionsButtonLeft.GetComponent<Button> ();
		instructionsButtonRight_text = instructionsButtonRight_text.GetComponent<Text> ();
		instructionsButtonLeft_text = instructionsButtonLeft_text.GetComponent<Text> ();
		instructionsButtonLeft_image = instructionsButtonLeft_image.GetComponent<Image> ();
		instructionsButtonRight_image = instructionsButtonRight_image.GetComponent<Image> ();
		instructions_page1_text = instructions_page1_text.GetComponent<Text>();
		instructions_page2_text = instructions_page2_text.GetComponent<Text>();
		instructions_page3_text = instructions_page3_text.GetComponent<Text>();
		instructions_page3_image = instructions_page3_image.GetComponent<Image>();

		showMenu ();
	}

	public void launchGame() {
		Application.LoadLevel ("Main");
	}

	public void showMenu()
	{
		changeAboutScreenState (false);
		hideInstructionsScreen ();
		changeMenuState (true);
	}

	public void showAboutScreen()
	{
		changeInstructionsScreenState (-1);
		changeMenuState (false);
		changeAboutScreenState (true);
	}

	public void showInstructionsScreen()
	{
		changeMenuState (false);
		changeAboutScreenState (false);
		changeInstructionsScreenState (0);
	}

	void changeInstructionsButtonRightState( bool state )
	{
		instructionsButtonRight.enabled = state;
		instructionsButtonRight_text.enabled = state;
		instructionsButtonRight_image.enabled = state;
	}

	void changeInstructionsButtonLeftState( bool state )
	{
		instructionsButtonLeft.enabled = state;
		instructionsButtonLeft_text.enabled = state;
		instructionsButtonLeft_image.enabled = state;
	}

	void hideInstructionsScreen()
	{
		changeInstructionsScreenState (-1);
		instructionsScreenState = 0;
	}

	public void instructionsScreen_navRight()
	{
		changeInstructionsScreenState (++instructionsScreenState);
	}

	public void instructionsScreen_navLeft()
	{
		changeInstructionsScreenState (--instructionsScreenState);
	}

	void changeInstructionsScreenState( int state )
	{
		//-1 = deactivate all open
		//0 = first screen
		//1 = second
		//2 = third

		if (state == -1) {
			changeBackButtonState (false);
			changeInstructionsButtonLeftState (false);
			changeInstructionsButtonRightState (false);
			instructionsScreenText.enabled = false;
			instructions_page1_text.enabled = false;
			instructions_page2_text.enabled = false;
			instructions_page3_text.enabled = false;
			instructions_page3_image.enabled = false;

		} else {
			changeInstructionsScreenState(-1);//Deactivate all open instruction screens
			instructionsScreenText.enabled = true;
			changeBackButtonState (true);
		}

		switch (state) {

		case 0:
		{
			changeInstructionsButtonRightState(true);
			changeInstructionsButtonLeftState(false);
			instructions_page1_text.enabled = true;
			break;
		}

		case 1:
		{
			changeInstructionsButtonLeftState(true);
			changeInstructionsButtonRightState(true);
			instructions_page2_text.enabled = true;
			break;
		}

		case 2:
		{
			changeInstructionsButtonLeftState(true);
			changeInstructionsButtonRightState(false);
			instructions_page3_text.enabled = true;
			instructions_page3_image.enabled = true;
			break;
		}

		}//end switch
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
