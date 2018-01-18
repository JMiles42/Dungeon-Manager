using System;

[Serializable]
public class Tile
{
	public TileType TileType;

	public bool IsWalkable() => IsWalkable(this);

	public static bool IsWalkable(Tile tile)
	{
		if(tile == null)
			return false;
		switch(tile.TileType)
		{
			case TileType.Empty:
				return false;
			case TileType.Floor:
				return true;
			default:
				return false;
		}
	}
}