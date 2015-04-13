﻿using UnityEngine;
using System.Collections;

public class FinishedShapeDetector : MonoBehaviour {

	ShapeFinder shapeFinder;

	bool finishedRemovingBlocks = true;

	public void removeFinishedShapes()
	{
		if (!GameController.accessGameController ().blockInPlay) {
			finishedRemovingBlocks = false;
			while( !finishedRemovingBlocks )
			{
				finishedRemovingBlocks = true;
				GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
				int numBlocks = blocks.Length;
				if( blocks.Length > 0 )
				{
					foreach( GameObject obj in blocks )
					{
						Block block = obj.GetComponent<BlockScript>().block;
						int search = shapeFinder.findShape(block, shapeFinder.map_Iblock);
						if( numBlocks != GameObject.FindGameObjectsWithTag("Block").Length )
							break;
						if( search != -1 )
						{
							Debug.Log ("Found I block");
							finishedRemovingBlocks = false;
							continue;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Sblock);
						if( search != -1 )
						{
							Debug.Log ("Found S block");
							finishedRemovingBlocks = false;
							continue;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Zblock);
						if( search != -1 )
						{
							Debug.Log ("Found Z block");
							finishedRemovingBlocks = false;
							continue;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Lblock);
						if( search != -1 )
						{
							Debug.Log ("Found L block");
							finishedRemovingBlocks = false;
							continue;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Jblock);
						if( search != -1 )
						{
							Debug.Log ("Found J block");
							finishedRemovingBlocks = false;
							continue;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Oblock);
						if( search != -1 )
						{
							Debug.Log ("Found O block");
							finishedRemovingBlocks = false;
							continue;
						}
						search = shapeFinder.findShape(block, shapeFinder.map_Tblock);
						if( search != -1 )
						{
							Debug.Log ("Found T block");
							finishedRemovingBlocks = false;
							continue;
						}
					}
				}
			}
			if( !GameController.accessGameController().allBlocksSettled() )
			{
				GameController.accessGameController().settleBlocks();
			}
		}
	}

	void Start()
	{
		shapeFinder = new ShapeFinder ();
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
