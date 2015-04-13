using UnityEngine;
using System.Collections;

public class Shape {

	string colour;
	string shapeType;
	int value;
	bool _givesScoreMultiplier;

	//type - 0 = i, 1 = j, 2 = l, 3 = o, 4 = s, 5 = t, 6 = z
	public static int[] finishedShapePointValues = { 5, 8, 8, 7, 9, 7, 9 };  

	public Shape( string colour, string shapeType )
	{
		this.colour = colour;
		this.shapeType = shapeType;
		this.value = ValueOfShapeType (shapeType);
		_givesScoreMultiplier = false;
	}

	public bool givesScoreMultiplier()
	{
		return _givesScoreMultiplier;
	}

	public void giveScoreMultiplier()
	{
		_givesScoreMultiplier = true;
	}

	public string getColour()
	{
		return colour;
	}

	public string getShapeType()
	{
		return shapeType;
	}

	public int getValue()
	{
		return value;
	}

	public static int ValueOfShapeType( string type )
	{
		int ret = 0;
		switch (type)
		{
			case "I":
			case "i":
			{
				ret = 5;
				break;
			}

			case "J":
			case "L":
			case "j":
			case "l":
			{
				ret = 8;
				break;
			}

			case "O":
			case "T":
			case "o":
			case "t":
			{
				ret = 7;
				break;
			}

			case "S":
			case "Z":
			case "s":
			case "z":
			{
				ret = 9;
				break;
			}
		}
		return ret;
	}
}
