using UnityEngine;
using System.Collections;

public class ShapeFinder {

	public Vector2[,] map_Iblock;
	public Vector2[,] map_Sblock;
	public Vector2[,] map_Zblock;
	public Vector2[,] map_Jblock;
	public Vector2[,] map_Lblock;
	public Vector2[,] map_Tblock;
	public Vector2[,] map_Oblock;

	public ShapeFinder()
	{
		//I block
		map_Iblock = new Vector2[,] {
			//---XX--- --------
			//---XX--- --------
			//---XX--- XXXXXXXX
			//---XX--- XXXXXXXX
			//---XX--- --------
			//---XX--- --------
			{

				new Vector2( 0.0f, 1.0f ),
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 0.0f, 1.0f )
			},
			{
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 1.0f, 0.0f )
			}
		};
		//S block
		map_Sblock = new Vector2[,] {
			//--XX---- --------
			//--XX---- ---XXXXX
			//--XXXX-- ---XXXXX
			//--XXXX-- -XXXXX--
			//----XX-- -XXXXX--
			//----XX-- --------
			{
				new Vector2( 0.0f, 1.0f ),
				new Vector2( -1.0f, 0.0f ),
				new Vector2( 0.0f, 1.0f )
			},
			{
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 1.0f, 0.0f )
			}
		};
		//Z block
		map_Zblock = new Vector2[,] {
			//----XX-- --------
			//----XX-- XXXXX---
			//--XXXX-- XXXXX---
			//--XXXX-- --XXXXX-
			//--XX---- --XXXXX-
			//--XX---- --------
			{
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 0.0f, 1.0f )
			},
			{
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 0.0f, -1.0f ),
				new Vector2( 1.0f, 0.0f )
			}
		};
		//J block
		map_Jblock = new Vector2[,] {
			//----XX-- -------- --XXXX-- --------
			//----XX-- XX------ --XXXX-- XXXXXXXX
			//----XX-- XX------ --XX---- XXXXXXXX
			//----XX-- XXXXXXXX --XX---- ------XX
			//--XXXX-- XXXXXXXX --XX---- ------XX
			//--XXXX-- -------- --XX---- --------
			{
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 0.0f, 1.0f )
			},
			{
				new Vector2( 0.0f, -1.0f ),
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 1.0f, 0.0f )
			},
			{
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 1.0f, 0.0f )
			},
			{
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 0.0f, -1.0f )
			}
		};
		//L block
		map_Lblock = new Vector2[,] {
			//--XX---- -------- --XXXX-- --------
			//--XX---- XXXXXXXX --XXXX-- ------XX
			//--XX---- XXXXXXXX ----XX-- ------XX
			//--XX---- XX------ ----XX-- XXXXXXXX
			//--XXXX-- XX------ ----XX-- XXXXXXXX
			//--XXXX-- -------- ----XX-- --------
			{
				new Vector2( -1.0f, 0.0f ),
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 0.0f, 1.0f )
			},
			{
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 1.0f, 0.0f )
			},
			{
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 0.0f, -1.0f ),
				new Vector2( 0.0f, -1.0f )
			},
			{
				new Vector2( 0.0f, -1.0f ),
				new Vector2( -1.0f, 0.0f ),
				new Vector2( -1.0f, 0.0f )
			}
		};
		//T block
		map_Tblock = new Vector2[,] {
			//--XX---- -------- ----XX-- --------
			//--XX---- XXXXXXXX ----XX-- ---XX---
			//--XXXX-- XXXXXXXX --XXXX-- ---XX---
			//--XXXX-- ---XX--- --XXXX-- XXXXXXXX
			//--XX---- ---XX--- ----XX-- XXXXXXXX
			//--XX---- -------- ----XX-- --------
			{
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 1.0f, -1.0f )
			},
			{
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 1.0f, 0.0f ),
				new Vector2( -1.0f, -1.0f )
			},
			{
				new Vector2( 0.0f, -1.0f ),
				new Vector2( 0.0f, -1.0f ),
				new Vector2( -1.0f, 1.0f )
			},
			{
				new Vector2( -1.0f, 0.0f ),
				new Vector2( -1.0f, 0.0f ),
				new Vector2( 1.0f, 1.0f )
			}
		};
		//O block
		map_Oblock = new Vector2[,] {
			//--------
			//--XXXX--
			//--XXXX--
			//--XXXX--
			//--XXXX--
			//--------
			{
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 1.0f, 0.0f ),
				new Vector2( 0.0f, -1.0f )
			}
		};
	}

	public int iWatcher;
	public int jWatcher;
	public int findShape(Block block, Vector2[,] map)
	{
		//Returns -1 if block was not found

		//Else returns int signifying in what orientation the shape was found
		//0 = UP
		//1 = RIGHT
		//2 = DOWN
		//3 = LEFT	

		int searchResult = -1;
		
		if( FinishedShapeDetector.blockTypeNearby( block ) )
		{
			int connections = 0;                                                                                                                                                                                                                
			Block current = new Block(true), next = new Block(true);
			current = block;
			for( int i = 0; i < map.GetLength(0); i++ )
			{
				connections = 0;
				for( int j = 0; j < map.GetLength(1); j++ )
				{
					//IM NOT SURE HOW ASSIGNING WORKS IN C# IT MIGHT FUCK UP SO IF SHIT GOES CRAZY LOOK AT WHAT NEXT IS CHANGING
					Vector2 nextVector = map[i,j];
					nextVector += current.blickPos;
					if( Blick.isInBlickArray(nextVector) )
					{
						if( !Block.checkCustom(nextVector) )
					   	{
							next = GameController.accessGameController().blickGrid[ (int)nextVector.x, (int)nextVector.y].getBlock();
							if( Block.CompareTypes( current, next ) )
							{
								connections++;
								current = next;
								if( connections == 3 )
								{
									searchResult = i;
									break;
								}
							}
							else
							{
								break;
							}
						} 
						else
						{
							break;
						}
					}
				}
				if( connections == 3 )
				{
					break;
				}
			}

			if( searchResult != -1 )
			{
				//Remove blocks if shape was found
				current = block;
				for( int i = 0; i < 4; i++ )
				{
					if( i < 3 )
					{	
						Vector2 nextVector = map[searchResult,i] + current.blickPos;
						next = GameController.accessGameController().blickGrid[ (int)nextVector.x, (int)nextVector.y].getBlock();
					}
					current.removeBlock();
					current = null;
					current = next;
				}
			}
		}
		return searchResult;
		
	}

}
