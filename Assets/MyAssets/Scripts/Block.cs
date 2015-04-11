using UnityEngine;
using System.Collections;

public class Block {

	int type;
	bool inPlay;
	public static int numBlockTypes = 7;
	public static float size = 0.75f;
	public Vector2 blickPos;
	public static Vector2 startPos = new Vector2 (-2.625f, -4.375f);
	
	
	//Stores value of finishing shapes with blocks
	//type - 0 = i, 1 = j, 2 = l, 3 = o, 4 = s, 5 = t, 6 = z
	public static int[] finishedShapePointValues = { 5, 8, 8, 7, 9, 7, 9 };  
	public static Color[] blockColors = { new Color( 255, 135, 0 ), //Orange
		new Color( 0, 0, 255 ), //Dark Blue
		new Color( 0, 255, 0 ), //Green
		new Color( 255, 255, 0 ), //Yellow
		new Color( 255, 0, 0 ), //Red
		new Color( 255, 0, 255 ), //Pink
		new Color( 0, 255, 255 ) //Cyan
	};
	
	public Block() : this( Random.Range(0,numBlockTypes) ){
		//If no parameters, create block at one of the spawnpoints with random type
	}
	
	public Block( Vector2 pos ) : this( pos, Random.Range(0,numBlockTypes) ){
		//If position parameter, create block at position with random type
	}
	
	public Block( int type ) : this( GameController.accessGameController().findAvailableSpawnPoint(), Random.Range(0,numBlockTypes) ){
		//if only type parameter, create block at one of the spawnpoints with specified type
	}
	
	public Block( Vector2 pos, int type )
	{
		blickPos = pos;
		this.type = type;
		inPlay = false;
		//Set corresponding Blick to "occupied"
		updateBlick ();
	}
	
	public bool isInPlay()
	{
		return inPlay;
	}
	
	public int getType()
	{
		return type;
	}
	
	public void update()
	{
		Blick thisBlock = getBlick ();
		if (blickPos.y != 0) 
		{
				//Get space beneath block to check if it should continue falling
				Blick t = GameController.accessGameController().blickGrid [(int)blickPos.x, (int)(blickPos.y - 1)];
				if (!t.isOccupied())
				{
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
		if (inPlay && thisBlock.isSettled ()) {
			inPlay = false;
			GameController.accessGameController().blockInPlay = false;
		}
	}
	
	void updateBlick()
	{
		Blick thisBlick = getBlick ();
		if (!thisBlick.isOccupied ()) {
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

		}
	}
}
