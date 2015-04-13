using UnityEngine;
using System.Collections;

public class ScoreboardController : MonoBehaviour {

	public Canvas scoreboard;
	public static bool _enabled = true;
	
	// Use this for initialization
	void Start () {
		scoreboard = scoreboard.GetComponent<Canvas> ();
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
