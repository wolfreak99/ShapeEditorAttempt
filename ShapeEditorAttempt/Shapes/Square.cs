using System.Drawing;
using System.Drawing.Drawing2D;

namespace ShapeEditorAttempt
{
	public class Square : Shape
	{
		public new const string NAME = "Square";
		public new const Shapes TYPE = Shapes.Square;

		public Square(int x, int y, int width, int height, Color color) : base(x, y, width, height, color)
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

			graphics.FillRectangle(pen.Brush, pos);
		}

		public override ShapeClickAction GetPointOverShapeAction(GraphicsPath path, Point point)
		{
			if (IsPointOverShape(path, point))
			{
				// Determine if not overlapping border, and drag. otherwise resize.
				path.Reset();
				path.AddRectangle(Rectangle.Inflate(position, -Shape.EDGE_WIDTH, -Shape.EDGE_WIDTH));
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
			path.AddRectangle(position);
			return path.IsVisible(point);
		}
		
		public override string GetShapeName()
		{
			return NAME;
		}

		public override Shapes GetShapeType()
		{
			return TYPE;
		}
	}
}
