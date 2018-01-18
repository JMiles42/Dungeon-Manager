using System;

[Serializable]
public class Room
{
	public const int SIZE = 10;
	public const int TOTAL_SIZE = SIZE * SIZE;

	public Tile[] Tiles = new Tile[TOTAL_SIZE];
}