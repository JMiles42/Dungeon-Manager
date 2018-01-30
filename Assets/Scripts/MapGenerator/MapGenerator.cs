using UnityEngine;
using Random = System.Random;

public static class MapGenerator
{
	public static Map Generate(MapSettings settings)
	{
		var board = new Map();
		Generate(board, settings);
		return board;
	}

	public static void Generate(Map map, MapSettings settings)
	{
		var rng = SetUpRandom(settings);

		GenerateTilesArray(map, settings, rng);
		GenerateRoomsAndCorridors(map, settings, rng);

		SetMapData(map);
	}

	private static Random SetUpRandom(MapSettings settings) => new Random(settings.Seed.GetHashCode());

	private static void GenerateTilesArray(Map map, MapSettings settings, Random rng)
	{
		map.tiles = new Column[settings.columns];

		for(var i = 0; i < map.tiles.Length; i++)
		{
			map.tiles[i] = new Column
							 {
								 tiles = new TileType[settings.rows]
							 };
		}
	}

	private static void GenerateRoomsAndCorridors(Map map, MapSettings settings, Random rng)
	{
		map.rooms = new Room[settings.numRooms.Random(rng)];

		map.corridors = new Corridor[map.rooms.Length - 1];

		map.rooms[0] = new Room();
		map.corridors[0] = new Corridor();

		GenerateFirstRoom(map.rooms[0], settings.roomWidth, settings.roomHeight, settings.columns, settings.rows, rng);

		GenerateCorridor(map.corridors[0],
						 map.rooms[0],
						 settings.corridorLength,
						 settings.roomWidth,
						 settings.roomHeight,
						 settings.columns,
						 settings.rows,
						 true,
						 rng);

		for(var i = 1; i < map.rooms.Length; i++)
		{
			map.rooms[i] = new Room();

			GenerateRoom(map.rooms[i], settings.roomWidth, settings.roomHeight, settings.columns, settings.rows, map.corridors[i - 1], rng);

			if(i < map.corridors.Length)
			{
				map.corridors[i] = new Corridor();

				GenerateCorridor(map.corridors[i],
								 map.rooms[i],
								 settings.corridorLength,
								 settings.roomWidth,
								 settings.roomHeight,
								 settings.columns,
								 settings.rows,
								 false,
								 rng);
			}
		}
	}

	private static void SetMapData(Map map)
	{
		SetMapDataFromRooms(map);
		SetMapDataFromCorridors(map);
	}

	private static void SetMapDataFromCorridors(Map map)
	{
		for(var i = 0; i < map.corridors.Length; i++)
		{
			var currentCorridor = map.corridors[i];

			for(var j = 0; j < currentCorridor.corridorLength; j++)
			{
				var xCoord = currentCorridor.startXPos;
				var yCoord = currentCorridor.startYPos;

				switch(currentCorridor.direction)
				{
					case Direction.North:
						yCoord += j;
						break;
					case Direction.East:
						xCoord += j;
						break;
					case Direction.South:
						yCoord -= j;
						break;
					case Direction.West:
						xCoord -= j;
						break;
				}

				map.tiles[xCoord][yCoord] = TileType.Floor;
			}
		}
	}

	private static void SetMapDataFromRooms(Map map)
	{
		for(var i = 0; i < map.rooms.Length; i++)
		{
			var currentRoom = map.rooms[i];

			for(var j = 0; j < currentRoom.roomWidth; j++)
			{
				var xCoord = currentRoom.xPos + j;

				for(var k = 0; k < currentRoom.roomHeight; k++)
				{
					var yCoord = currentRoom.yPos + k;

					map.tiles[xCoord][yCoord] = TileType.Floor;
				}
			}
		}
	}

	public static void GenerateCorridor(
			Corridor corridor,
			Room room,
			IntRange length,
			IntRange roomWidth,
			IntRange roomHeight,
			int columns,
			int rows,
			bool firstCorridor,
			Random rng)
	{
		corridor.direction = (Direction)rng.Next(0, 4);

		var oppositeDirection = (Direction)(((int)room.enteringCorridor + 2) % 4);

		if(!firstCorridor && (corridor.direction == oppositeDirection))
		{
			var directionInt = (int)corridor.direction;
			directionInt++;
			directionInt = directionInt % 4;
			corridor.direction = (Direction)directionInt;
		}

		corridor.corridorLength = length.Random(rng);

		var maxLength = length.Max;

		switch(corridor.direction)
		{
			case Direction.North:
				corridor.startXPos = rng.Next(room.xPos, (room.xPos + room.roomWidth) - 1);
				corridor.startYPos = room.yPos + room.roomHeight;
				maxLength = rows - corridor.startYPos - roomHeight.Min;
				break;
			case Direction.East:
				corridor.startXPos = room.xPos + room.roomWidth;
				corridor.startYPos = rng.Next(room.yPos, (room.yPos + room.roomHeight) - 1);
				maxLength = columns - corridor.startXPos - roomWidth.Min;
				break;
			case Direction.South:
				corridor.startXPos = rng.Next(room.xPos, room.xPos + room.roomWidth);
				corridor.startYPos = room.yPos;
				maxLength = corridor.startYPos - roomHeight.Min;
				break;
			case Direction.West:
				corridor.startXPos = room.xPos;
				corridor.startYPos = rng.Next(room.yPos, room.yPos + room.roomHeight);
				maxLength = corridor.startXPos - roomWidth.Min;
				break;
		}

		corridor.corridorLength = Mathf.Clamp(corridor.corridorLength, 1, maxLength);
	}

	public static void GenerateRoom(Room room, IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor, Random rng)
	{
		room.enteringCorridor = corridor.direction;

		room.roomWidth = widthRange.Random(rng);
		room.roomHeight = heightRange.Random(rng);

		switch(corridor.direction)
		{
			case Direction.North:
				room.roomHeight = Mathf.Clamp(room.roomHeight, 1, rows - corridor.EndPositionY);
				room.yPos = corridor.EndPositionY;

				room.xPos = rng.Next((corridor.EndPositionX - room.roomWidth) + 1, corridor.EndPositionX);
				room.xPos = Mathf.Clamp(room.xPos, 0, columns - room.roomWidth);
				break;
			case Direction.East:
				room.roomWidth = Mathf.Clamp(room.roomWidth, 1, columns - corridor.EndPositionX);
				room.xPos = corridor.EndPositionX;

				room.yPos = rng.Next((corridor.EndPositionY - room.roomHeight) + 1, corridor.EndPositionY);
				room.yPos = Mathf.Clamp(room.yPos, 0, rows - room.roomHeight);
				break;
			case Direction.South:
				room.roomHeight = Mathf.Clamp(room.roomHeight, 1, corridor.EndPositionY);
				room.yPos = (corridor.EndPositionY - room.roomHeight) + 1;

				room.xPos = rng.Next((corridor.EndPositionX - room.roomWidth) + 1, corridor.EndPositionX);
				room.xPos = Mathf.Clamp(room.xPos, 0, columns - room.roomWidth);
				break;
			case Direction.West:
				room.roomWidth = Mathf.Clamp(room.roomWidth, 1, corridor.EndPositionX);
				room.xPos = (corridor.EndPositionX - room.roomWidth) + 1;

				room.yPos = rng.Next((corridor.EndPositionY - room.roomHeight) + 1, corridor.EndPositionY);
				room.yPos = Mathf.Clamp(room.yPos, 0, rows - room.roomHeight);
				break;
		}
	}

	public static void GenerateFirstRoom(Room room, IntRange widthRange, IntRange heightRange, int columns, int rows, Random rng)
	{
		room.roomWidth = widthRange.Random(rng);
		room.roomHeight = heightRange.Random(rng);

		room.xPos = Mathf.RoundToInt((columns / 2f) - (room.roomWidth / 2f));
		room.yPos = Mathf.RoundToInt((rows / 2f) - (room.roomHeight / 2f));
	}
}