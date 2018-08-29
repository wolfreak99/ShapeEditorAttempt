using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeEditorAttempt
{
	public class Circle : Shape
	{
		public Circle(int x, int y, int width, int height, Color color) : base(x, y, width, height, color)
		{
		}

		public override void Draw(Canvas sender, Graphics graphics)
		{
			Rectangle pos;
			if (sender.clickedShape == this)
			{
				pos = PreviewOffset(position, sender.clickedShapeAction);
			}
			else
			{
				pos = position;
			}

			graphics.FillEllipse(pen.Brush, pos);
		}

		public override ShapeClickAction GetPointOverShapeAction(GraphicsPath path, Point point)
		{
			if (IsPointOverShape(path, point))
			{
				// Determine if not overlapping border, and drag. otherwise resize.
				path.Reset();
				path.AddEllipse(Rectangle.Inflate(position, -Shape.EDGE_WIDTH, -Shape.EDGE_WIDTH));
				if (path.IsVisible(point))
					return ShapeClickAction.Drag;
				else
					return ShapeClickAction.Resize;
			}
			else
			{
				return ShapeClickAction.None;
			}
		}

		public override bool IsPointOverShape(GraphicsPath path, Point point)
		{
			// Determine if overlapping border, and resize.
			path.Reset();
			path.AddEllipse(position);
			return path.IsVisible(point);
		}

		public override string GetShapeName()
		{
			return "Circle";
		}

		public override Shapes GetShapeType()
		{
			return Shapes.Circle;
		}
	}
}
