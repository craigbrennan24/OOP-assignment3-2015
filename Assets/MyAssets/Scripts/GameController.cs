using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Vector2[] spawnPoints;
	public Blick[,] blickGrid;
	public GameObject block;
	float blockSize;
	float lastFall = 3;
	int rows = 17;
	int cols = 8;
	int count = 0;
	bool blockInPlay = false;

	// Use this for initialization
	void Start () {
		setupSpawnPoints ();
		blickGrid = new Blick[2,2];
		blickGrid [0,0] = new Blick ();
	}

	bool BlockInPlay(){
		return blockInPlay;
	}

	void setupSpawnPoints()
	{
		spawnPoints = new Vector2[8];
		float inc = 0.75f;
		float start = -2.625f;

		for (int i = 0; i < cols; i++) {
			spawnPoints[i] = new Vector2(start + (inc*i), 7.625f);
		}
		Instantiate (block, spawnPoints [0], Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastFall > 0.5f) {
			if( count < 15 )
			{
				GameObject[] g = GameObject.FindGameObjectsWithTag("Block");
				foreach (GameObject obj in g) {
					obj.transform.position = new Vector3 (obj.transform.position.x, obj.transform.position.y - Blick.size, 0);
				}
				count++;
				lastFall = Time.time;
			}
		}
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
		bool ret = false;
		if (Input.touchCount == 1) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			Vector2 touchPos = new Vector2 (wp.x, wp.y);
			if (col == Physics2D.OverlapPoint (touchPos)) {
				ret = true;
			}
		}
		return ret;
	}
}

public class Blick
{
	//Blicks are points in space that keep track of and hold the falling blocks.
	//They do not need vectors because the place they sit in the array they are held in
	//corresponds to the point they represent on the board.
	//They also hold misc data
	public static float size = 0.75f;
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
}
