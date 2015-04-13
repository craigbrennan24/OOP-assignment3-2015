using UnityEngine;
using System.Collections;

public class Block {

	int type;
	bool controlledByPlayer;
	public static int numBlockTypes = 7;
	public static float size = 0.75f;
	public Vector2 blickPos;
	public static Vector2 startPos = new Vector2 (-2.625f, -4.375f);
	
	
	//Stores value of finishing shapes with blocks
	//type - 0 = i, 1 = j, 2 = l, 3 = o, 4 = s, 5 = t, 6 = z
	public static int[] finishedShapePointValues = { 5, 8, 8, 7, 9, 7, 9 };  
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
		return controlledByPlayer;
	}

	public void givePlayerControl()
	{
		controlledByPlayer = true;
	}

	public void removePlayerControl ()
	{
		controlledByPlayer = false;
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
		if ( blickPos.y != 0 )
		{
			//Get space beneath block to check if it should continue falling
			Blick underBlock = GameController.accessGameController().blickGrid [(int)blickPos.x, (int)(blickPos.y - 1)];
			if ( !underBlock.isOccupied() ){
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
			GameController.accessGameController().blockInPlay = false;
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
	
	public void fall()
	{
		Blick thisBlock = getBlick ();
		if (!thisBlock.isSettled ()) {
			if( Time.time - GameController.lastFall > GameController.fallDelay )
			{
				if( checkDown() )
				{
					moveBlock( new Vector2( blickPos.x, (blickPos.y-1) ) );
					GameController.lastFall = Time.time;
				}
			}
		}
	}


	//CHECK NEARBY BLICK METHODS
	//ALL METHODS RETURN TRUE IF BLICK IS EMPTY, FALSE IF NOT
	public static bool checkCustom(Vector2 dest)
	{
		bool ret = false;
		if (dest.x >= 0 && dest.x <= (GameController.cols - 1)) {
			if( dest.y >= 0 && dest.y <= (GameController.rows -1 ) ) {
				Blick other = GameController.accessGameController().blickGrid [(int)dest.x, (int)dest.y];
				if( !other.isOccupied() )
					ret = true;
			}
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
