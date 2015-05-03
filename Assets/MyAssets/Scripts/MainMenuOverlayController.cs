using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuOverlayController : MonoBehaviour {

	public Button startButton;
	public Button instructionsButton;
	public Button aboutButton;
	public Button exitButton;
	public Text startButtonText;
	public Text instructionsButtonText;
	public Text aboutButtonText;
	public Text exitButtonText;

	// Use this for initialization
	void Start () {
		startButton = startButton.GetComponent<Button> ();
		instructionsButton = instructionsButton.GetComponent<Button> ();
		aboutButton = aboutButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
		startButtonText = startButtonText.GetComponent<Text> ();
		instructionsButtonText = instructionsButtonText.GetComponent<Text> ();
		aboutButtonText = aboutButtonText.GetComponent<Text> ();
		exitButtonText = exitButtonText.GetComponent<Text> ();
	}

	public void launchGame() {
		Application.LoadLevel ("Main");
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
