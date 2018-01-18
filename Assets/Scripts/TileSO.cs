using System;
using JMiles42.ScriptableObjects.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileSO: GenericFoldoutScriptableObject<Tile>
{ }

public static class TileSOExtn
{
	public static bool IsWalkable(this TileSO tile)
	{
		if(tile == null)
			return false;
		switch(tile.Data.TileType)
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