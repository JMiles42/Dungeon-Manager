using System;

[Serializable]
public class Map
{
	public Corridor[] corridors;
	public Room[] rooms;
	public Column[] tiles;
}

[Serializable]
public class Column
{
	public TileType[] tiles;

	public int Length => tiles.Length;
	public int Count => tiles.Length;

	public TileType this[int i]
	{
		get { return tiles[i]; }
		set { tiles[i] = value; }
	}
}