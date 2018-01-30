using JMiles42.Editor.Utilities;
using JMiles42.Grid;
using UnityEditor;
using UnityEngine;

public static class MapGizmos
{
	[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
	public static void MapGizmoDrawer(MapCreator creator, GizmoType type)
	{
		if(!creator.Map)
			return;
		if(creator.Map.Data?.tiles == null)
			return;
		for(var x = 0; x < creator.Map.Data.tiles.Length; x++)
		{
			var dataTile = creator.Map.Data.tiles[x];
			for(var y = 0; y < dataTile.Length; y++)
			{
				var tile = dataTile[y];
				switch(tile)
				{
					case TileType.Wall:
						using(EditorDisposables.ColorChanger(Color.blue, EditorColourType.Gizmos))
							Gizmos.DrawCube(new GridPosition(x, y), Vector3.one);
						break;
					case TileType.Floor:
						using(EditorDisposables.ColorChanger(Color.green, EditorColourType.Gizmos))
							Gizmos.DrawCube(new GridPosition(x, y), Vector3.one);
						break;
				}
			}
		}
	}
}