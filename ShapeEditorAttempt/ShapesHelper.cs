using System;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class ShapesHelper
	{
		public static Shape CreateNewShape(int x, int y, int width, int height, Color color, Shapes shape)
		{
			switch (shape)
			{
			case Shapes.Square:
				return new Square(x, y, width, height, color);
			case Shapes.Circle:
				return new Circle(x, y, width, height, color);
			case Shapes.Triangle:
				return new Triangle(x, y, width, height, color);
			default:
				throw new NotImplementedException("Shapes." + Enum.GetName(shape.GetType(), shape) + " not yet implemented");
			}
		}
	}
}
