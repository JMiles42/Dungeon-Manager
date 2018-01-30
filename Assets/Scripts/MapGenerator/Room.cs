using System;

[Serializable]
public class Room
{
	public Direction enteringCorridor; // The direction of the corridor that is entering this room.
	public int roomHeight; // How many tiles high the room is.
	public int roomWidth; // How many tiles wide the room is.
	public int xPos; // The x coordinate of the lower left tile of the room.
	public int yPos; // The y coordinate of the lower left tile of the room.
}