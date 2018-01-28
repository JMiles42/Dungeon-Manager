using System;
using System.Collections.Generic;
using JMiles42.Attributes;

[Serializable]
public class Map
{
	[NoFoldout] public Size MapSize = new Size(100);

	[NoFoldout] public List<PassageWay> PassageWays = new List<PassageWay>();

	[NoFoldout] public List<Room> Rooms = new List<Room>();

	public static Map Default { get; } = new Map();

	internal void Clear()
	{
		MapSize = Size.One * 100;
		PassageWays.Clear();
		Rooms.Clear();
	}
}