using System;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class ShapesHelper
	{
		static public Shape CreateNewShape(int x, int y, int width, int height, Color color, Shapes shape)
		{
			switch (shape)
			{
			case Shapes.Square:
				return new Square(x, y, width, height, color);
			case Shapes.Circle:
				return new Circle(x, y, width, height, color);
			case Shapes.Triangle:
				return new Triangle(x, y, width * 2, height * 2, color); // Multiply twice to ensure reaches border
			default:
				throw new NotImplementedException("Shapes." + Enum.GetName(shape.GetType(), shape) + " not yet implemented");
			}
		}

		static public string GetShapeName(Shapes shape)
		{
			switch (shape)
			{
			case Shapes.Square:
				return Square.NAME;
			case Shapes.Circle:
				return Circle.NAME;
			case Shapes.Triangle:
				return Triangle.NAME;
			default:
				throw new NotImplementedException("Shapes." + Enum.GetName(shape.GetType(), shape) + " not yet implemented");
			}
		}
	}
}
