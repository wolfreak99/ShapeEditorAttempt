using System;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public partial class ShapeTypeHelper
	{
		static public Shape CreateNewShape(int x, int y, int width, int height, Color color, ShapeType shape)
		{
			switch (shape)
			{
			case ShapeType.Square:
				return new Square(x, y, width, height, color);
			case ShapeType.Circle:
				return new Circle(x, y, width, height, color);
			case ShapeType.Triangle:
				return new Triangle(x, y, width, height, color); // Multiply twice to ensure reaches border
			default:
				throw new ShapeTypeNotSupportedException(shape);
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
				throw new ShapeTypeNotSupportedException(shape);
			}
		}
	}
}
