using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool canControlBlock = false;
	public float lastMoved = float.MaxValue;
	bool speedUp_flag = true;
	bool lastMoved_flag = true;

	void Update()
	{
		if (!GameController.paused) {
			if (GetComponent<GameController> ().blockInPlay) {
				checkButtons ();
				if (Time.time - lastMoved > 0.25) {
					lastMoved_flag = true;
				}
			}
		}
	}

	void checkButtons()
	{
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase != TouchPhase.Ended && Input.GetTouch (0).phase != TouchPhase.Canceled) {
				if (lastMoved_flag) {
					//Left
					if (GameController.playerIsTouching (GameObject.Find ("LeftButton").GetComponent<Collider2D> ())) {
						moveLeft ();
						lastMoved_flag = false;
						lastMoved = Time.time;
					}
					//Right
					else if (GameController.playerIsTouching (GameObject.Find ("RightButton").GetComponent<Collider2D> ())) {
						moveRight ();
						lastMoved_flag = false;
						lastMoved = Time.time;
					}
				}
				if (speedUp_flag) {
					if (GameController.playerIsTouching (GameObject.Find ("DownButton").GetComponent<Collider2D> ())) {
						quickFall ();
						speedUp_flag = false;
					}
				}
			}
		} else if (Input.GetKey ("left")) {
			if (lastMoved_flag) {
				moveLeft ();
				lastMoved_flag = false;
				lastMoved = Time.time;
			}
		} else if (Input.GetKey ("right")) {
			if (lastMoved_flag) {
				moveRight ();
				lastMoved_flag = false;
				lastMoved = Time.time;
			}
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			GameController.accessGameController().togglePause();
		}
		else {
			lastMoved_flag = true;
			speedUp_flag = true;
		}
	}


	public void quickFall()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			if (GameController.fallDelay == GameController.FallSpeed.normal) {
				GameController.fallDelay = GameController.FallSpeed.fast;
			} else {
				GameController.fallDelay = GameController.FallSpeed.normal;
			}
		}
	}



	public void moveRight()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			//Check if there is an empty spot to move to
			Block playerBlock = getPlayerBlock();
			if( playerBlock != null )
			{
				if( playerBlock.checkRight() )
				{
					Vector2 move = new Vector2( 1.0f, 0.0f );
					playerBlock.moveBlock( playerBlock.blickPos + move );
				}
			}
		}
	}

	public void moveLeft()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			//Check if there is an empty spot to move to
			Block playerBlock = getPlayerBlock();
			if( playerBlock != null )
			{
				if( playerBlock.checkLeft() )
				{
					Vector2 move = new Vector2( -1.0f, 0.0f );
					playerBlock.moveBlock( playerBlock.blickPos + move );
				}
			}
		}
	}

	public void moveUp()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			//Check if there is an empty spot to move to
			Block playerBlock = getPlayerBlock();
			if( playerBlock != null )
			{
				if( playerBlock.checkUp() )
				{
					Vector2 move = new Vector2( 0.0f, 1.0f );
					playerBlock.moveBlock( playerBlock.blickPos + move );
				}
			}
		}
	}

	public void moveDown()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			//Check if there is an empty spot to move to
			Block playerBlock = getPlayerBlock();
			if( playerBlock != null )
			{
				if( playerBlock.checkDown() )
				{
					Vector2 move = new Vector2( 0.0f, -1.0f );
					playerBlock.moveBlock( playerBlock.blickPos + move );
				}
			}
		}
	}

	public Block getPlayerBlock()
	{
		Block block = null;
		if (GetComponent<GameController> ().blockInPlay) {
			GameObject[] objects = GameObject.FindGameObjectsWithTag("Block");
			foreach( GameObject obj in objects )
			{
				if( obj.GetComponent<BlockScript>().block != null )
				{
					if( obj.GetComponent<BlockScript>().block.isControlledByPlayer() )
					{
						block = obj.GetComponent<BlockScript>().block;
						break;
					}
				}
			}
		}
		return block;
	}



}
