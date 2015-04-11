using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Vector2[] spawnPoints;
	public Vector2[,] blockPositions;
	public Blick[,] blickGrid;
	public GameObject blockSpawner;
	public static bool _gameOver = false;
	//Testing: _spawnNextAs will tell the block script what it should spawn itself as, then revert it to -1.
	//-1 = random color at random spawnpoint
	//0 - 6 = specified color at random spawnpoint
	public static int _spawnNextAs = 0;
	public const int rows = 17;
	public const int cols = 8;
	bool blockInPlay = false;

	// Use this for initialization
	void Start () {
		spawnPoints = GetComponent<SetupScript>().setupSpawnPoints ();
		blockPositions = GetComponent<SetupScript>().setupBlockPositions ();
		blickGrid = GetComponent<SetupScript>().setupBlicks ();
		Instantiate (blockSpawner, new Vector3 (0, 0, 0), Quaternion.identity);
	}

	bool BlockInPlay(){
		return blockInPlay;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public Vector2 findAvailableSpawnPoint()
	{
		Vector2 ret = new Vector2 ();
		if (spawnPointExists ()) {
			//Check if spawn point is empty, find another if not
			bool spawnPointValid = false;
			int x = 0;
			while( !spawnPointValid )
			{
				x = Random.Range (0,cols);
				if( !blickGrid[x, (rows-1)].isOccupied() )
				{
					spawnPointValid = true;
				}
			}
			ret = new Vector2(x, (rows-1));
		}
		return ret;
	}

	public bool spawnPointExists()
	{
		bool ret = false;
		for (int i = 0; i < cols; i++) {
			if( !blickGrid[i,(rows-1)].isOccupied() )
			{
				ret = true;
			}
		}
		return ret;
	}

	void OnGUI()
	{
		drawText ();
	}

	void drawText()
	{
		string touchPos = "touch: ";
		string blockPos = "block: ";
		GameObject tObj = GameObject.FindGameObjectWithTag("Block");
		if (Input.touchCount == 1) {
			Vector3 t = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).position );
			Vector2 t1 = new Vector2( t.x, t.y );
			touchPos += t1.x + "," + t1.y;
		}
		if (tObj != null) {
			Vector2 t = new Vector2( tObj.transform.position.x, tObj.transform.position.y);
			blockPos += t.x + "," + t.y;
		}
		float width = 200;
		GUI.TextArea (new Rect ((Screen.width / 2) - (width/2), (Screen.height / 2) - (width/2), width, width), touchPos + "\n" + blockPos);
	}

	void createStartingBlocks()
	{

	}

	public int getRows()
	{
		return rows;
	}

	public int getCols()
	{
		return cols;
	}

	public static bool playerIsTouching( Collider2D col )
	{
		//Find if the player is touching a gameobject's collider
		bool ret = false;
		if (Input.touchCount == 1) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			Vector2 touchPos = new Vector2 (wp.x, wp.y);
			if (col.GetComponent<Collider2D>() == Physics2D.OverlapPoint (touchPos)) {
				ret = true;
			}
		}
		return ret;
	}

	public bool allBlocksSettled()
	{
		/*bool ret = true;
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Block");
		foreach (GameObject obj in objects) {
			if ( blickGrid[][] )
		}*/
		return true;
	}
}

public class Blick
{
	//Blicks are points in space that keep track of and hold the falling blocks.
	//They do not need vectors because the place they sit in the array they are held in
	//corresponds to the point they represent on the board.
	//They also hold misc data
	public static float fallDelay = 1f;
	bool occupied;
	bool settled;


	public Blick()
	{
		this.occupied = false;
		this.settled = false;
	}

	public bool isOccupied()
	{
		return occupied;
	}

	public bool isSettled()
	{
		return settled;
	}

	public void setSettled( bool settled )
	{
		this.settled = settled;
	}

	public void setOccupied( bool occupied )
	{
		this.occupied = occupied;
	}
}
