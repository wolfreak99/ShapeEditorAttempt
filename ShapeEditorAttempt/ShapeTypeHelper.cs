using System;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public partial class ShapeTypeHelper
	{
		static public Shape CreateNewShape(int x, int y, int width, int height, Color color, ShapeType shape, bool stretchTriangle = true)
		{
			switch (shape)
			{
			case ShapeType.Square:
				return new Square(x, y, width, height, color);
			case ShapeType.Circle:
				return new Circle(x, y, width, height, color);
			case ShapeType.Triangle:
				if (stretchTriangle)
				{
					return new Triangle(x, y, width * 2, height * 2, color); // Multiply twice to ensure reaches border
				}
				else
				{
					return new Triangle(x, y, width, height, color);
				}
			default:
				throw ShapeTypeNotSupportedException.Throw(shape);
			}
		}

		static public string GetShapeName(ShapeType shape)
		{
			switch (shape)
			{
			case ShapeType.Square:
				return Square.NAME;
			case ShapeType.Circle:
				return Circle.NAME;
			case ShapeType.Triangle:
				return Triangle.NAME;
			default:
				throw ShapeTypeNotSupportedException.Throw(shape);
			}
		}
	}
}
