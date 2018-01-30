using System;
using JMiles42.Attributes;
using JMiles42.Maths.Random;
using UnityEngine;

[Serializable]
public class MapSettings
{
	[Range(0, 8)] public string Seed = RandomStrings.GetRandomString(8);
	[NoFoldout] public IntRange corridorLength = new IntRange(6, 10); // The range of lengths corridors between rooms can have
	[NoFoldout] public IntRange numRooms = new IntRange(15, 20); // The range of the number of rooms there can be.
	[NoFoldout] public IntRange roomHeight = new IntRange(3, 10); // The range of heights rooms can have.
	[NoFoldout] public IntRange roomWidth = new IntRange(3, 10); // The range of widths rooms can have.
	public int columns = 100; // The number of columns on the board (how wide it will be)
	public int rows = 100; // The number of rows on the board (how tall it will be).
}