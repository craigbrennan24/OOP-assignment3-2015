using UnityEngine;
using System.Collections;

public class Block {

	int type;
	bool controlledByPlayer;
	public static int numBlockTypes = 7;
	public static float size = 0.75f;
	public Vector2 blickPos;
	public static Vector2 startPos = new Vector2 (-2.625f, -4.375f);
	

	public static Color[] blockColors = { 
		new Color( 1.0f, 0.549f, 0.098f, 1.0f ), //Orange
		new Color( 0.0f, 0.0f, 1.0f, 1.0f ), //Dark Blue
		new Color( 0.0f, 1.0f, 0.0f, 1.0f ), //Green
		new Color( 1.0f, 0.92f, 0.016f, 1.0f ), //Yellow
		new Color( 1.0f, 0.0f, 0.0f, 1.0f ), //Red
		new Color( 1.0f, 0.0f, 1.0f, 1.0f ), //Pink
		new Color( 0.0f, 1.0f, 1.0f, 1.0f ) //Cyan
	};
	
	public Block() : this( Random.Range(0,numBlockTypes) ){
		//If no parameters, create block at one of the spawnpoints with random type
	}
	
	public Block( Vector2 pos ) : this( pos, Random.Range(0,numBlockTypes) ){
		//If position parameter, create block at position with random type
	}
	
	public Block( int type ) : this( GameController.accessGameController().findAvailableSpawnPoint(), type ){
		//if only type parameter, create block at one of the spawnpoints with specified type
	}

	public Block( bool hideMe )
	{
		//Use this constructor to create a dummy block object that will not be applied to game world
		blickPos = GameController._hiddenSpawn;
		controlledByPlayer = false;
	}
	
	public Block( Vector2 pos, int type )
	{
		if (Vector2.Equals (pos, new Vector2 (-1, -1)))
			GameController._gameOver = true;
		blickPos = pos;
		this.type = type;
		controlledByPlayer = false;
		//Set corresponding Blick to "occupied"
		if( !GameController._gameOver )
			updateBlick ();
	}

	public void moveBlock( Vector2 dest )
	{

		Blick oldBlick = getBlick ();
		oldBlick.setSettled (false);
		oldBlick.setOccupied (false);
		oldBlick.block = null;
		blickPos = dest;
		updateBlick ();
	}
	
	public bool isControlledByPlayer()
	{
		if (controlledByPlayer) {
			if( !checkDown() ) {
				Vector2 getBelow = new Vector2( 0.0f, -1.0f );
				getBelow += blickPos;
				Blick other = Blick.getBlick(getBelow);
				if( other != null )
				{
					if( other.isSettled() )
					{
						removePlayerControl();
					}
				}
			}
		}
		return controlledByPlayer;
	}

	public void givePlayerControl()
	{
		controlledByPlayer = true;
	}

	public void removePlayerControl ()
	{
		controlledByPlayer = false;
		GameController.accessGameController ().blockInPlay = false;
	}

	public int getType()
	{
		return type;
	}

	public void setType(int type)
	{
		this.type = type;
	}

	public void removeBlock()
	{
		Blick thisBlick = getBlick ();
		thisBlick.block = null;
		thisBlick.setSettled(false);
		thisBlick.setOccupied(false);
		GameObject thisBlock = getParentObject ();
		thisBlock.tag += "[delete]";
		thisBlock.GetComponent<BlockScript> ().Remove ();
	}

	public GameObject getParentObject()
	{
		GameObject ret;
		GameObject[] blocks = GameObject.FindGameObjectsWithTag ("Block");
		ret = null;
		foreach (GameObject block in blocks) {
			if( Vector2.Equals(blickPos,block.GetComponent<BlockScript>().block.blickPos) )
			{
				ret = block;
			}
		}
		return ret;
	}
	
	public void update()
	{
		Blick thisBlock = getBlick ();
		if (!GameController.accessGameController ().blockInPlay && isControlledByPlayer ()) {
			removePlayerControl();
		}
		if ( blickPos.y != 0 )
		{
			if ( checkDown() ){
				thisBlock.setSettled (false);
				fall ();
			}
			else
			{
				thisBlock.setSettled(true);
				updateBlick();
			}
		}
		else 
		{
			thisBlock.setSettled(true);
		}
		if ( isControlledByPlayer() && thisBlock.isSettled ()) {
			removePlayerControl();
		}
	}
	
	void updateBlick()
	{
		Blick thisBlick = getBlick ();
		if ( !thisBlick.isOccupied () ) {
			thisBlick.setOccupied(true);
			thisBlick.block = this;
		}
	}

	public Blick getBlick()
	{
		return GameController.accessGameController().blickGrid [(int)blickPos.x, (int)blickPos.y];
	}
	
	public Color getColor()
	{
		return blockColors [type];
	}

	public string getColorName()
	{
		string ret = "unknown";
		switch (type) {
		case 0:
			ret = "Orange";
			break;
		case 1:
			ret = "DarkBlue";
			break;
		case 2:
			ret = "Green";
			break;
		case 3:
			ret = "Yellow";
			break;
		case 4:
			ret = "Red";
			break;
		case 5:
			ret = "Pink";
			break;
		case 6:
			ret = "Cyan";
			break;
		}
		return ret;
	}
	
	public void fall()
	{
		Blick thisBlock = getBlick ();
		if (!thisBlock.isSettled ()) {
			if( isControlledByPlayer() )
			{
				if( Time.time - GameController.lastFall > GameController.fallDelay )
				{
					if( checkDown() )
					{
						moveBlock( new Vector2( blickPos.x, (blickPos.y-1) ) );
						GameController.lastFall = Time.time;
					}
				}
			}
			else
			{
				if( checkDown() )
				{
					moveBlock( new Vector2( blickPos.x, (blickPos.y-1) ) );
				}
			}
		}
	}


	//CHECK NEARBY BLICK METHODS
	//ALL METHODS RETURN TRUE IF BLICK IS EMPTY, FALSE IF NOT
	public static bool checkCustom(Vector2 dest)
	{
		bool ret = false;
		if ( Blick.isInBlickArray(dest) ) {
			Blick other = GameController.accessGameController().blickGrid [(int)dest.x, (int)dest.y];
			if( !other.isOccupied() )
				ret = true;
		}
		return ret;
	}

	public bool checkRight()
	{
		bool ret = false;
		if (blickPos.x < (GameController.cols - 1)) {
			Blick other = GameController.accessGameController ().blickGrid [(int)(blickPos.x + 1), (int)blickPos.y];
			if( !other.isOccupied() )
				ret = true;
		}
		return ret;
	}

	public bool checkLeft()
	{
		bool ret = false;
		if (blickPos.x > 0) {
			Blick other = GameController.accessGameController ().blickGrid [(int)(blickPos.x - 1), (int)blickPos.y];
			if( !other.isOccupied() )
				ret = true;
		}
		return ret;
	}

	public bool checkUp()
	{
		bool ret = false;
		if (blickPos.y < (GameController.rows - 1)) {
			Blick other = GameController.accessGameController ().blickGrid [(int)blickPos.x, (int)(blickPos.y + 1)];
			if (!other.isOccupied ())
				ret = true;
		}
		return ret;
	}

	public bool checkDown()
	{
		bool ret = false;
		if (blickPos.y > 0) {
			Blick other = GameController.accessGameController ().blickGrid [(int)blickPos.x, (int)(blickPos.y - 1)];
			if (!other.isOccupied ())
				ret = true;
		}
		return ret;
	}

	public static bool CompareTypes( Block one, Block two )
	{
		bool ret = false;
		if (one.getType () == two.getType ())
			ret = true;
		return ret;
	}
}
