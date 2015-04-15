using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	//Statics + constants
	public static Vector2 _hiddenSpawn = new Vector2 (0.0f, -20.0f);//Not sure if I'll keep this, use to spawn a block off screen before moving it to a custom location
	public static float fallDelay = 0.5f;//Current speed of falling blocks
	public static int score = 0;
	public static int scoreMultiplier = 1;//Increases if the player varies the types of shapes they complete (hard cap 12)
	public static int scoreMultiplier_combo = 1;//Increases when the player finishes shapes in succession ( 3 stages: 2.5x(rounded up), 5x, 10x )
	const int scoreMuliplier_cap = 8;
	const int scoreMultiplier_combo_cap = 3;
	public static float lastFall = float.MaxValue;//Last time a block was dropped
	public static bool _gameOver = false;
	public static bool paused = false;
	public const int rows = 17;
	public const int cols = 8;

	public class FallSpeed
	{
		public static float normal = 0.2f;//Default speed for all falling blocks
		public static float fast = 0.05f;//Speed the block falls when the user presses the quickfall button
		public static float max = 0f;//Highest speed possible, normal will slowly increment towards this as the game goes on.
	}

	//Ingame reference vectors
	public Vector2[] spawnPoints;
	public Vector2[,] blockPositions;
	public Blick[,] blickGrid;
	public Vector2 customBlockSpawner;

	//Flags and values
	List<Block> startBlocks = new List<Block>();
	int startLines;
		//These flags are used to allow gamesetup to run for the first few frames
		//without causing failures across the board because values that other scripts are
		//depending on haven't been initialized
		public bool _gameSetup;
		bool _gameSetup_1;
		bool _gameSetup_2;
		bool _gameSetup_3;
		bool _gameSetup_4;
		bool _gameSetup_firstPlay;
	bool _gameOver_showMenuFlag;//Tells the gameover menu to display
	public GameObject blockSpawner;//Blank block gameobject used when instantiating
	public bool customSpawn;//Set to true if you need to force a block to spawn at a custom location (ie, starting block)
	public bool blockInPlay;//True if there is a block falling that is controlled by the player
	public List<Shape> finishedShapes;//List of all finished shapes in the current game, used by scoreboard.

	// Use this for initialization
	void Start () {
		_gameSetup_firstPlay = true;
		setup ();
	}

	public void setup()
	{
		initializeValues ();
		if (_gameSetup_firstPlay) {
			spawnPoints = GetComponent<SetupScript> ().setupSpawnPoints ();
			blockPositions = GetComponent<SetupScript> ().setupBlockPositions ();
			_gameSetup_firstPlay = false;
		} else {
			clearBoard();
			GameObject.Find("InGameOverlay").GetComponent<InGameOverlayController>().hideGameOverMenu();
		}
		blickGrid = GetComponent<SetupScript>().setupBlicks ();
	}

	public void restart()
	{
		GameObject.Find ("InGameOverlay").GetComponent<InGameOverlayController> ().reset ();
		GetComponent<FinishedShapeDetector> ().reset ();
		setup ();
	}

	void initializeValues()
	{
		startBlocks = new List<Block> ();
		finishedShapes = new List<Shape> ();
		startLines = 1;
		score = 0;
		_gameOver = false;
		_gameSetup = true;
		_gameSetup_1 = true;
		_gameSetup_2 = true;
		_gameSetup_3 = true;
		_gameSetup_4 = true;
		_gameOver_showMenuFlag = true;
		customSpawn = false;
		blockInPlay = false;
		paused = false;
		Time.timeScale = 1;
	}

	void clearBoard(){
		GameObject[] blocks = GameObject.FindGameObjectsWithTag ("Block");
		foreach (GameObject block in blocks) {
			Destroy(block);
		}
	}

	// Update is called once per frame
	void Update () {
		if (!_gameOver) {
			if (_gameSetup) {
				//SETUP
				gameSetup();
			} else {
				//IN GAME
				if (!blockInPlay && allBlocksSettled ()) {
					GetComponent<FinishedShapeDetector>().removeFinishedShapes();
					dropNewBlock ();
				} else {

				}
			}
		} else {
			//GAME OVER
			if( _gameOver_showMenuFlag )
			{
				GameObject.Find("InGameOverlay").GetComponent<InGameOverlayController>().showGameOverMenu();
				_gameOver_showMenuFlag = false;
			}
		}
	}

	public void endGame()
	{
		_gameOver = true;
	}

	public void togglePause()
	{
		Text t = GameObject.Find ("InGameOverlay").GetComponent<InGameOverlayController> ().pauseButton.GetComponent<Text> ();
		InGameOverlayController overlay = GameObject.Find ("InGameOverlay").GetComponent<InGameOverlayController> ();
		if (Time.timeScale == 1) {
			t.fontSize = 27;
			t.text = "unpause";
			Time.timeScale = 0;
			paused = true;
			overlay.showOptionsMenu();
		} else {
			t.fontSize = 30;
			t.text = "pause";
			Time.timeScale = 1;
			paused = false;
			overlay.hideOptionsMenu();
		}
	}

	void gameSetup()
	{
		if (_gameSetup_1) {
			//FIRST PASS
			createStartingBlocks (startLines);
			_gameSetup_1 = false;
		} else if (_gameSetup_2) {
			//SECOND PASS
			startingBlocksPass ();
			if (!_gameSetup_3) {
				//THIRD PASS
				GetComponent<FinishedShapeDetector>().removeFinishedShapes();
				if(!_gameSetup_4)
				{
					//FOURTH PASS
					GetComponent<PlayerController>().lastMoved = Time.time;
					_gameSetup_2 = false;
					_gameSetup = false;
				}
				_gameSetup_4 = false;
			}
			_gameSetup_3 = false;
		}
	}
	
	void dropNewBlock()
	{
		customSpawn = false;
		Instantiate (blockSpawner, _hiddenSpawn, Quaternion.identity);
		blockInPlay = true;
		lastFall = Time.time;
		if (fallDelay != FallSpeed.normal)
			fallDelay = FallSpeed.normal;
	}

	public Vector2 findAvailableSpawnPoint()
	{
		Vector2 ret = new Vector2 ( -1, -1 );
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
		/*
		if( _gameOver )
			drawText ();
			*/
	}

	void drawText()
	{
		string touchPos = "touch: ";
		string gameOver = "GAME OVER";
		if (Input.touchCount == 1) {
			Vector3 t = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).position );
			Vector2 t1 = new Vector2( t.x, t.y );
			touchPos += t1.x + "," + t1.y;
		}
		float width = 200;
		GUI.TextArea (new Rect ((Screen.width / 2) - (width/2), (Screen.height / 2) - (width/2), width, width/2), touchPos + "\n\n\n" + gameOver );
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

	public void addScore( Shape shape )
	{
		//Determines the worth of finished shape, applies appropriate multipliers and then adds to total score. Also adds shape to list of finished shapes for the scoreboard.
		int scoreInc;
		if (finishedShapes.Count > 0) {
			if( shape.getShapeType() != finishedShapes[finishedShapes.Count-1].getShapeType() )
			{
				//If starting a chain multiplier
				if( scoreMultiplier <= scoreMuliplier_cap )
				{
					//Dont increment multiplier above the cap
					scoreMultiplier ++;
				}
				shape.activateScoreMultiplier();
			}
			else
				scoreMultiplier = 1;
		}
		scoreInc = shape.getValue () * scoreMultiplier;
		FinishedShapeDetector f = GetComponent<FinishedShapeDetector> ();
		if (f.finishedShapeLastTurn) {
			//If starting a combo
			if (f.comboCounter > 0) {
				if (f.comboCounter < scoreMultiplier_combo_cap)
					scoreMultiplier_combo = f.comboCounter;
				else
					scoreMultiplier_combo = scoreMultiplier_combo_cap;
			} else
				scoreMultiplier_combo = 1;
			scoreInc += applycombo (scoreInc);
			shape.activateComboMultiplier();
		} else
			scoreMultiplier_combo = 0;
		finishedShapes.Add (shape);
		score += scoreInc;
	}

	int applycombo( int shapeScore )
	{
		//used in addScore() to multiply the gained score by the combo multiplier before adding to total score
		int ret = 0;
		if (scoreMultiplier_combo == 1) {
			float halfShapeScore = ((float)(shapeScore)) * 2.5f;
			ret = Mathf.CeilToInt (halfShapeScore);
		} else if (scoreMultiplier_combo == 2) {
			ret = shapeScore * 5;
		} else if (scoreMultiplier_combo == 3) {
			ret = shapeScore * 10;
		}
		return ret;
	}

	public void settleBlocks()
	{
		//Use this to force a block update if needed
		GameObject[] blocks = GameObject.FindGameObjectsWithTag ("Block");
		while (!allBlocksSettled()) {
			foreach( GameObject block in blocks )
			{
				block.GetComponent<BlockScript>().block.update();
				block.GetComponent<BlockScript>().verifyPosColor();
				break;
			}
		}
	}

	public static GameController accessGameController()
	{
		//'return this;' does not work because I want it to be static :(
		return GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}
}