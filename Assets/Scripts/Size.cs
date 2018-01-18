using System;
using JMiles42.Types;

[Serializable]
public struct Size
{
	public int Width;
	public int Height;

	public Size(int size)
	{
		Width = size;
		Height = size;
	}

	public Size(int width, int height)
	{
		Width = width;
		Height = height;
	}

	public static implicit operator Size(int input) => new Size(input);
	public static implicit operator Size(Vector2I input) => new Size(input.x, input.y);
	public static implicit operator Vector2I(Size input) => new Vector2I(input.Width, input.Height);
}