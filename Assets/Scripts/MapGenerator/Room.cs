using System;
using JMiles42.Attributes;
using JMiles42.Grid;
[Serializable]
public class Room
{
	public GridPosition Position;
	[NoFoldout] public Size Dimensions;

	public Room()
	{
		Position = GridPosition.Zero;
		Dimensions = Size.One;
	}

	public Room(GridPosition position, Size dimensions)
	{
		Position = position;
		Dimensions = dimensions;
	}
}