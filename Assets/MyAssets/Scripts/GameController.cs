using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Vector2[] spawnPoints;
	ArrayList startBlocks = new ArrayList();
	int startLines;
	public static Vector2 _hiddenSpawn = new Vector2 (0.0f, -20.0f);
	public Vector2[,] blockPositions;
	public bool _gameSetup;
	bool _gameSetup_1;
	bool _gameSetup_2;
	bool _gameSetup_3;
	public Blick[,] blickGrid;
	public GameObject blockSpawner;
	public static bool _gameOver = false;
	public bool customSpawn;
	public Vector2 customBlockSpawner;
	public const int rows = 17;
	public const int cols = 8;
	public bool blockInPlay;

	// Use this for initialization
	void Start () {
		initializeValues ();
		spawnPoints = GetComponent<SetupScript>().setupSpawnPoints ();
		blockPositions = GetComponent<SetupScript>().setupBlockPositions ();
		blickGrid = GetComponent<SetupScript>().setupBlicks ();
	}

	void initializeValues()
	{
		startBlocks = new ArrayList ();
		startLines = 5;
		_gameSetup = true;
		_gameSetup_1 = true;
		_gameSetup_2 = true;
		_gameSetup_3 = true;
		customSpawn = false;
		blockInPlay = false;

	}

	bool BlockInPlay(){
		return blockInPlay;
	}

	// Update is called once per frame
	void Update () {
		if (_gameSetup) {
			//SETUP
			if (_gameSetup_1) {
				createStartingBlocks (startLines);
				_gameSetup_1 = false;
			} else if (_gameSetup_2) {
				startingBlocksPass ();
				if( !_gameSetup_3 )
				{
					_gameSetup_2 = false;
					_gameSetup = false;
				}
				_gameSetup_3 = false;
			}
		} else {
			if( !blockInPlay && allBlocksSettled() )
			{
				dropNewBlock();
			}
			else
			{
			}
		}
	}

	void dropNewBlock()
	{
		customSpawn = false;
		Instantiate (blockSpawner, _hiddenSpawn, Quaternion.identity);
		blockInPlay = true;
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
		//drawText ();
	}

	void drawText()
	{
		string touchPos = "touch: ";
		if (Input.touchCount == 1) {
			Vector3 t = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).position );
			Vector2 t1 = new Vector2( t.x, t.y );
			touchPos += t1.x + "," + t1.y;
		}
		float width = 200;
		GUI.TextArea (new Rect ((Screen.width / 2) - (width/2), (Screen.height / 2) - (width/2), width, width/2), touchPos );
	}

	void startingBlocksPass()
	{
		//Use this method to attempt to randomize the starting pile so that there is a lesser chance for blocks of the same
		//type to spawn next to each other.
		for( int j = 0; j < startLines; j++ )
		{
			for( int i = 0 ; i < cols; i++ )
			{
				if( i != (cols-1) )
				{
					Block one, two;
					one = getObjectAtBlickPos(new Vector2( (float)i, (float)j )).GetComponent<BlockScript>().block;
					two = getObjectAtBlickPos(new Vector2( (float)(i+1), (float)j )).GetComponent<BlockScript>().block;
					if( one.getType() == two.getType() )
					{
						one.setType(weightedTypeChoice(one.getType()));
					}
				}
			}
		}
	}

	public GameObject getObjectAtBlickPos( Vector2 blickPos )
	{
		GameObject[] blocks = GameObject.FindGameObjectsWithTag ("Block");
		GameObject ret = null;
		foreach (GameObject block in blocks) {
			if( Vector2.Equals(block.GetComponent<BlockScript>().block.blickPos, blickPos ) )
			{
				ret = block;
				break;
			}
		}
		return ret;
	}

	void createStartingBlocks( int startLines )
	{
		/*customSpawn = true;
		for (int j = 0; j < startLines; j++) {
			for( int i = 0; i < cols; i++ )
			{
				customBlockSpawner = new Vector2( i, j );
				Instantiate (blockSpawner, customBlockSpawner, Quaternion.identity);
			}
		}*/

		int i, j;
		//if newchain is true block will spawn taking into consideration the colour of the blocks next to them
		bool newChain = false;
		customSpawn = true;
		for( j = 0; j < startLines; j++)
		{
			for( i = 0; i < 8; i++)
			{
				if( !newChain )
				{
					//if starting new block placement without consideration of
					//previous block colours
					customBlockSpawner = new Vector2( (float)i, (float)j );
					Instantiate( blockSpawner, customBlockSpawner, Quaternion.identity );
					blickGrid[i,j].setSettled(true);
				}
				else
				{
					int previousType = -1;
					int newType;
					Block temp;
					Vector2 t = new Vector2( (float)(i-1), (float)j );
					for( int k = 0; k < startBlocks.Count; k++ )
					{
						temp = (Block)startBlocks[k];
						if( Vector2.Equals( t, temp.blickPos ) )
						{
							previousType = temp.getType();
							break;
						}
					}
					newType = Random.Range(0,7);
					if( previousType == newType )
					{
						newType = weightedTypeChoice( previousType );
					}
					customBlockSpawner = new Vector2( (float)i, (float)j );
					startBlocks.Add (new Block(customBlockSpawner, newType));
				}
			}
		}
		if (startBlocks.Count > 0) {
			foreach( Block block in startBlocks )
			{
				Instantiate( blockSpawner, block.blickPos, Quaternion.identity );
				blickGrid[ (int)block.blickPos.x, (int)block.blickPos.y ].setSettled(true);
			}
		}

	}

	public int weightedTypeChoice( int prev )
	{
		int[] colours = new int[] { 0, 1, 2, 3, 4, 5, 6 };
		int[] chance = new int[] { 10, 10, 10, 10, 10, 10, 10 };
		chance[prev] -= 10;
		int choose;
		int total = 0;
		for( int i = 0; i < chance.GetLength(0); i++ )
		{
			total += chance[i];
		}
		
		choose = Random.Range (0,total);
		choose++;
		int type = 0;
		int holder = 0;
		for( int i = 0; i < chance.GetLength(0); i++ )
		{
			holder += chance[i];
			if( choose <= holder )
			{
				type = colours[i];
				break;
			}
		}
		return type;
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
		bool ret = true;
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Block");
		foreach (GameObject obj in objects) {
			if( !obj.GetComponent<BlockScript>().block.getBlick().isSettled() )
			{
				ret = false;
				break;
			}
		}
		return ret;
	}

	public static GameController accessGameController()
	{
		//'return this;' does not work because I want it to be static :(
		return GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}
}