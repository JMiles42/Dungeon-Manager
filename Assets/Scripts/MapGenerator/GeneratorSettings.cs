using System;
using JMiles42.Attributes;
using UnityEngine;
using Random = System.Random;

[Serializable]
public class GeneratorSettings
{
	[Range(0, 8)]
	public string Seed;
	[NoFoldout(true)] public IntMinMax ExtraPassageWayCount = new IntMinMax
															  {
																  Min = 4,
																  Max = 10
															  };

	[NoFoldout(true)] public IntMinMax RoomCount = new IntMinMax
												   {
													   Min = 4,
													   Max = 10
												   };

	[NoFoldout(true)] public IntMinMax RoomSize = new IntMinMax
												   {
													   Min = 5,
													   Max = 20
												   };

	[NoFoldout(true)] public Size TotalGridSize = new Size(100);
}

[Serializable]
public struct IntMinMax
{
	[Half10Line] public int Min;
	[Half01Line] public int Max;

	public int GetRandomNumber(Random png) => png.Next(Min, Max);
}

[Serializable]
public struct FloatMinMax
{
	[Half10Line] public float Min;
	[Half01Line] public float Max;

	public float GetRandomNumber(Random png) => ((float)png.NextDouble() * (Max - Min)) + Min;
}