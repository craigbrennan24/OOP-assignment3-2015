using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool canControlBlock = false;
	public float lastMoved = float.MaxValue;
	bool lastMoved_flag = true;

	void Update()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			checkButtons();
			if( Time.time - lastMoved > 0.25 )
			{
				lastMoved_flag = true;
			}
		}
	}

	void checkButtons()
	{
		if( Input.touchCount > 0 )
		{
			if( lastMoved_flag )
			{
				//Left
				if (GameController.playerIsTouching (GameObject.Find ("LeftButton").GetComponent<Collider2D> ())) {
					moveLeft();
					lastMoved_flag = false;
					lastMoved = Time.time;
				}
				//Right
				else if( GameController.playerIsTouching(GameObject.Find("RightButton").GetComponent<Collider2D>()) )
				{

					moveRight();
					lastMoved_flag = false;
					lastMoved = Time.time;
				}
			}
		}
	}

	public void moveRight()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			//Check if there is an empty spot to move to
			Block playerBlock = getPlayerBlock();
			if( playerBlock.checkRight() )
			{
				Vector2 move = new Vector2( 1.0f, 0.0f );
				playerBlock.moveBlock( playerBlock.blickPos + move );
			}
		}
	}

	public void moveLeft()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			//Check if there is an empty spot to move to
			Block playerBlock = getPlayerBlock();
			if( playerBlock.checkLeft() )
			{
				Vector2 move = new Vector2( -1.0f, 0.0f );
				playerBlock.moveBlock( playerBlock.blickPos + move );
			}
		}
	}

	public void moveUp()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			//Check if there is an empty spot to move to
			Block playerBlock = getPlayerBlock();
			if( playerBlock.checkUp() )
			{
				Vector2 move = new Vector2( 0.0f, 1.0f );
				playerBlock.moveBlock( playerBlock.blickPos + move );
			}
		}
	}

	public void moveDown()
	{
		if (GetComponent<GameController> ().blockInPlay) {
			//Check if there is an empty spot to move to
			Block playerBlock = getPlayerBlock();
			if( playerBlock.checkDown() )
			{
				Vector2 move = new Vector2( 0.0f, -1.0f );
				playerBlock.moveBlock( playerBlock.blickPos + move );
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
				if( obj.GetComponent<BlockScript>().block.isControlledByPlayer() )
				{
					block = obj.GetComponent<BlockScript>().block;
					break;
				}
			}
		}
		return block;
	}



}
