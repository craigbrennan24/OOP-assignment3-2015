using UnityEngine;
using System.Collections;

public class Blick {

	//Blicks are points in space that keep track of and hold the falling blocks.
	//They do not need vectors because the place they sit in the array they are held in
	//corresponds to the point they represent on the board.
	//They also hold misc data
	bool occupied;
	bool settled;
	public Block block;
	
	
	public Blick()
	{
		this.occupied = false;
		this.settled = false;
		block = null;
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

	public Block getBlock()
	{
		return block;
	}

	public static bool isInBlickArray( Vector2 dest )
	{
		bool ret = false;
		if (dest.x >= 0 && dest.x <= (GameController.cols - 1)) {
			if (dest.y >= 0 && dest.y <= (GameController.rows - 1)) {
				ret = true;
			}
		}
		return ret;
	}
}
