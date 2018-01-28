using JMiles42.Grid;
using Rand = System.Random;

public static class Generator
{
	private const int TOP_EDGE = 5;

	public static Map GenerateMap(GeneratorSettings settings)
	{
		var map = Map.Default;
		GenerateMap(ref map, settings);
		return map;
	}

	public static void GenerateMap(ref Map map, GeneratorSettings settings)
		{
		map.Clear();
		var png = new Rand(settings.Seed.GetHashCode());
		GenerateRooms(ref png, ref map, ref settings);
		GeneratePassageWays(ref png, ref map, ref settings);

		//Generate Grid Size
		//Generate Rooms
		//Generate Passage Ways
		//Calculate Start Room
		//Detail Rooms
	}

	private static void GenerateRooms(ref Rand png, ref Map map, ref GeneratorSettings settings)
	{
		var roomCount = settings.RoomCount.GetRandomNumber(png);
		map.Rooms.Capacity = roomCount;
		for(var i = 0; i < roomCount; i++)
		{
			var room = new Room();
			room.Position = new GridPosition(png.Next(0, settings.TotalGridSize.Width - TOP_EDGE), png.Next(0, settings.TotalGridSize.Height - TOP_EDGE));
			room.Dimensions = new Size(settings.RoomSize.GetRandomNumber(png), settings.RoomSize.GetRandomNumber(png));

			map.Rooms.Add(room);
		}
	}

	private static void GeneratePassageWays(ref Rand png, ref Map map, ref GeneratorSettings settings)
	{ }
}