using JMiles42.Editor.Utilities;
using JMiles42.Generics;
using JMiles42.Grid;
using UnityEditor;
using UnityEngine;

public static class MapGizmos
{
	[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
	public static void MapGizmoDrawer(GeneratorComponent generator, GizmoType type)
	{
		if(!generator.Map)
			return;
		if(generator.Map.Data == null)
			return;

		//DrawMainGrid(generator.Map);
		DrawRooms(generator.Map);
		DrawPassageWays(generator.Map);
	}

	private static void DrawMainGrid(MapSO Map)
	{
		//Main Grid
		Array2DHelpers.ForLoop2D(Map.Data.MapSize, DrawCube);
	}

	private static void DrawRooms(MapSO Map)
	{
		using(EditorDisposables.ColorChanger(Color.red, EditorColourType.Gizmos))
				//Rooms
		{
			for(var i = 0; i < Map.Data.Rooms.Count; i++)
				Array2DHelpers.ForLoop2D(Map.Data.Rooms[i].Dimensions.ToVector2I() + Map.Data.Rooms[i].Position, Map.Data.Rooms[i].Position, DrawCube);
		}
	}

	private static void DrawPassageWays(MapSO Map)
	{
		//Passage Ways
		//Array2DHelpers.ForLoop2D(generator.Map.Data.MapSize, DrawCube);
	}

	private static void DrawCube(int x, int y)
	{
		Gizmos.DrawCube(new GridPosition(x, y), Vector3.one * 0.99f);
	}
}