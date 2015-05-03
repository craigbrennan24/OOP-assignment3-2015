using UnityEngine;
using System.Collections;

public class FinishedShapeDetector : MonoBehaviour {

	ShapeFinder shapeFinder;

	bool finishedRemovingBlocks = true;
	public bool finishedShapeLastTurn = false;
	bool finishedShapeThisTurn = false;
	public int comboCounter;

	public void removeFinishedShapes( bool checkForCombos )
	{
		if( checkForCombos )
			finishedShapeThisTurn = false;
		if (!GameController.accessGameController ().blockInPlay) {
			if( checkForCombos )
				finishedRemovingBlocks = false;
			while( !finishedRemovingBlocks )
			{
				if( checkForCombos )
					finishedRemovingBlocks = true;
				GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
				int numBlocks = blocks.Length;
				if( blocks.Length > 0 )
				{
					foreach( GameObject obj in blocks )
					{
						if( numBlocks != GameObject.FindGameObjectsWithTag("Block").Length )
							break;
						Block block = obj.GetComponent<BlockScript>().block;
						string shapeColour = block.getColorName();
						string debugS = "Found shape! Color: " + block.getColorName() + " Type: ";
						int search = shapeFinder.findShape(block, shapeFinder.map_Iblock);
						if( search != -1 )
						{
							finishedRemovingBlocks = false;
							GameController.accessGameController().addScore( new Shape(shapeColour, "I" ) );
							debugS += "I";
							Debug.Log(debugS);
							if( checkForCombos )
							{
								finishedShapeThisTurn = true;
								if( finishedShapeLastTurn )
									comboCounter ++;
							}
							break;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Sblock);
						if( search != -1 )
						{
							GameController.accessGameController().addScore( new Shape(shapeColour, "S" ) );
							finishedRemovingBlocks = false;
							debugS += "S";
							Debug.Log(debugS);
							if( checkForCombos )
							{
								finishedShapeThisTurn = true;
								if( finishedShapeLastTurn )
									comboCounter ++;
							}
							break;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Zblock);
						if( search != -1 )
						{
							GameController.accessGameController().addScore( new Shape(shapeColour, "Z" ) );
							finishedRemovingBlocks = false;
							debugS += "Z";
							Debug.Log(debugS);
							if( checkForCombos )
							{
								finishedShapeThisTurn = true;
								if( finishedShapeLastTurn )
									comboCounter ++;
							}
							break;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Lblock);
						if( search != -1 )
						{
							GameController.accessGameController().addScore( new Shape(shapeColour, "L" ) );
							finishedRemovingBlocks = false;
							debugS += "L";
							Debug.Log(debugS);
							if( checkForCombos )
							{
								finishedShapeThisTurn = true;
								if( finishedShapeLastTurn )
									comboCounter ++;
							}
							break;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Jblock);
						if( search != -1 )
						{
							GameController.accessGameController().addScore( new Shape(shapeColour, "J" ) );
							finishedRemovingBlocks = false;
							debugS += "J";
							Debug.Log(debugS);
							if( checkForCombos )
							{
								finishedShapeThisTurn = true;
								if( finishedShapeLastTurn )
									comboCounter ++;
							}
							break;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Oblock);
						if( search != -1 )
						{
							GameController.accessGameController().addScore( new Shape(shapeColour, "O" ) );
							finishedRemovingBlocks = false;
							debugS += "O";
							Debug.Log(debugS);
							if( checkForCombos )
							{
								finishedShapeThisTurn = true;
								if( finishedShapeLastTurn )
									comboCounter ++;
							}
							break;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Tblock);
						if( search != -1 )
						{
							GameController.accessGameController().addScore( new Shape(shapeColour, "T" ) );
							finishedRemovingBlocks = false;
							debugS += "T";
							Debug.Log(debugS);
							if( checkForCombos )
							{
								finishedShapeThisTurn = true;
								if( finishedShapeLastTurn )
									comboCounter ++;
							}
							break;
						}
					}
				}
			}
			if( !GameController.accessGameController().allBlocksSettled() )
			{
				GameController.accessGameController().settleBlocks();
			}
			if( checkForCombos )
			{
				if (!finishedShapeThisTurn)
					comboCounter = 0;
				finishedShapeLastTurn = finishedShapeThisTurn;
			}
		}
	}

	void Start()
	{
		shapeFinder = new ShapeFinder ();
		comboCounter = 0;
	}

	public void reset()
	{
		finishedShapeLastTurn = false;
		comboCounter = 0;
	}

	public static bool blockTypeNearby( Block block )
	{
		bool ret = false;
		//Checks 4 surrounding horizontal and vertical blocks for same colour types
		if ( Blick.isInBlickArray(new Vector2((block.blickPos.x-1), block.blickPos.y)) ) {
			if (!block.checkLeft ()) {
				Block other = GameController.accessGameController ().blickGrid [(int)(block.blickPos.x - 1), (int)block.blickPos.y].getBlock ();
				if (other.getType () == block.getType ())
					return true;
			}
		}
		if (Blick.isInBlickArray (new Vector2 ((block.blickPos.x + 1), block.blickPos.y))) {
			if (!block.checkRight ()) {
				Block other = GameController.accessGameController ().blickGrid [(int)(block.blickPos.x + 1), (int)block.blickPos.y].getBlock ();
				if (other.getType () == block.getType ())
					return true;
			}
		}
		if (Blick.isInBlickArray (new Vector2 (block.blickPos.x, (block.blickPos.y + 1)))) {
			if (!block.checkUp ()) {
				Block other = GameController.accessGameController ().blickGrid [(int)block.blickPos.x, (int)(block.blickPos.y + 1)].getBlock ();
				if (other.getType () == block.getType ())
					return true;
			}
		}
		if (Blick.isInBlickArray (new Vector2 (block.blickPos.x, (block.blickPos.y - 1)))) {
			if (!block.checkDown ()) {
				Block other = GameController.accessGameController ().blickGrid [(int)block.blickPos.x, (int)(block.blickPos.y - 1)].getBlock ();
				if (other.getType () == block.getType ())
					return true;
			}
		}
		return ret;
	}
}
