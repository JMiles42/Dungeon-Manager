using System;
using JMiles42.Attributes;
using JMiles42.Types;

[Serializable]
public struct Size
{
	[Half10Line] public int Width;
	[Half01Line] public int Height;

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

	public static Size operator -(Size left) => new Size(-left.Width, -left.Height);
	public static Size operator +(Size left) => new Size(+left.Width, +left.Height);

	public static Size operator -(Size left, Size right) => new Size(left.Width - right.Width, left.Height - right.Height);
	public static Size operator +(Size left, Size right) => new Size(left.Width + right.Width, left.Height + right.Height);
	public static Size operator *(Size left, Size right) => new Size(left.Width * right.Width, left.Height * right.Height);
	public static Size operator /(Size left, Size right) => new Size(left.Width / right.Width, left.Height / right.Height);

	public Vector2I ToVector2I() => this;

	public static implicit operator Size(int input) => new Size(input);
	public static implicit operator Size(Vector2I input) => new Size(input.x, input.y);
	public static implicit operator Vector2I(Size input) => new Vector2I(input.Width, input.Height);

	public static Size Zero { get; } = new Size(0);
	public static Size One { get; } = new Size(1);
}