using System;
using JMiles42.Attributes;

[Serializable]
public class GeneratorSettings
{
	[NoFoldout(true)]
	public Size TotalGridSize = new Size(100);
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

}

[Serializable]
public struct IntMinMax
{
	public int Min;
	public int Max;
}

[Serializable]
public struct FloatMinMax
{
	public float Min;
	public float Max;
}