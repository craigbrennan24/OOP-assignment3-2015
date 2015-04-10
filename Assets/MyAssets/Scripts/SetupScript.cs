using UnityEngine;
using System.Collections;

public class SetupScript : MonoBehaviour {

	//Sets up misc vectors and values for game controller script

	public Vector2[,] setupBlockPositions()
	{
		Vector2[,] blockPositions = new Vector2[GameController.cols, GameController.rows];
		for (int i = 0; i < blockPositions.GetLength(0); i++) {
			for( int j = 0; j < blockPositions.GetLength(1); j++ ) {
				blockPositions[i,j] = new Vector2( BlockScript.Block.startPos.x + (BlockScript.Block.size * i ), BlockScript.Block.startPos.y + (BlockScript.Block.size * j) );
			}
		}
		return blockPositions;
	}

	public Blick[,] setupBlicks()
	{
		Blick[,] blickGrid = new Blick[GameController.cols,GameController.rows];
		for( int i = 0 ; i < GameController.cols; i++ )
		{
			for( int j = 0; j < GameController.rows; j++ )
			{
				blickGrid[i,j] = new Blick();
			}
		}
		return blickGrid;
	}

	public Vector2[] setupSpawnPoints()
	{
		Vector2[] spawnPoints = new Vector2[8];
		float inc = 0.75f;
		float start = -2.625f;
		
		for (int i = 0; i < GameController.cols; i++) {
			spawnPoints[i] = new Vector2(start + (inc*i), 7.625f);
		}
		return spawnPoints;
	}
}
