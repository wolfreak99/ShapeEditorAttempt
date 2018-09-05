using System.Drawing;
using System.Drawing.Drawing2D;

namespace ShapeEditorAttempt
{
	public class Square : Shape
	{
		new public const string NAME = "Square";
		new public const ShapeType TYPE = ShapeType.Square;
		override public string Name { get { return NAME; } }
		override public ShapeType Type { get { return TYPE; } }

		public Square(int x, int y, int width, int height, Color color) : base(x, y, width, height, color)
		{
		}

		public override void Draw(Canvas sender, Graphics graphics)
		{
			Rectangle pos = (ClickData.Shape == this) ? PreviewOffset(position, ClickData.Action) : position;
			graphics.FillRectangle(pen.Brush, pos);
		}

		public override ShapeClickAction GetShapeActionByPoint(GraphicsPath path, Point point)
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
	}
}
