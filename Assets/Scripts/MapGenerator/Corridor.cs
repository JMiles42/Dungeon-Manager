using System;

// Enum to specify the direction is heading.
public enum Direction
{
	North,
	East,
	South,
	West
}

[Serializable]
public class Corridor
{
	public int corridorLength; // How many units long the corridor is.
	public Direction direction; // Which direction the corridor is heading from it's room.
	public int startXPos; // The x coordinate for the start of the corridor.
	public int startYPos; // The y coordinate for the start of the corridor.

	// Get the end position of the corridor based on it's start position and which direction it's heading.
	public int EndPositionX
	{
		get
		{
			if((direction == Direction.North) || (direction == Direction.South))
				return startXPos;
			if(direction == Direction.East)
				return (startXPos + corridorLength) - 1;
			return (startXPos - corridorLength) + 1;
		}
	}

	public int EndPositionY
	{
		get
		{
			if((direction == Direction.East) || (direction == Direction.West))
				return startYPos;
			if(direction == Direction.North)
				return (startYPos + corridorLength) - 1;
			return (startYPos - corridorLength) + 1;
		}
	}
}