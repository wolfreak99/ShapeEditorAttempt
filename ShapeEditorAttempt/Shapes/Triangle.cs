using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ShapeEditorAttempt
{
	public class Triangle : Shape
	{
		public enum Angle
		{
			TopLeft,
			TopCenter,
			TopRight,
			BottomLeft,
			BottomCenter,
			BottomRight,
			Length
		}

		new public const string NAME = "Triangle";
		new public const ShapeType TYPE = ShapeType.Triangle;
		new public const int EDGE_WIDTH = 8;
		override public string Name { get { return NAME; } }
		override public ShapeType Type { get { return TYPE; } }
		override public int EdgeWidth { get { return EDGE_WIDTH; } }
		
		private const Angle DEFAULT_ANGLE = Angle.TopLeft;
		public Angle angle { get; set; } = DEFAULT_ANGLE;

		public Triangle(int x, int y, int width, int height, Color color, Angle angle) : base(x, y, width, height, color)
		{
			this.angle = angle;
		}

		public void IncrementAngle()
		{
			angle = (Angle)Utils.WrapIncrement(angle, 1, 0, Angle.Length);
		}

		// Link any constructors without angles to the default angle
		public Triangle(int x, int y, int width, int height, Color color) : base (x, y, width, height, color)
		{
			this.angle = DEFAULT_ANGLE;
		}

		public override void DrawShape(Graphics graphics, Rectangle position)
		{
			Point[] points = GetPointsByAngle(angle, position);
			graphics.FillPolygon(Pen.Brush, points);
		}

		public override void DrawBorder(Graphics graphics, Rectangle position)
		{
			Point[] points = GetPointsByAngle(angle, position);
			graphics.DrawPolygon(Pen, points);
		}

		static private Point[] GetPointsByAngle(Angle triangleAngle, Rectangle position)
		{
			switch (triangleAngle)
			{
			case Angle.TopLeft:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Left, position.Bottom)
				};
			case Angle.TopCenter:
				return new Point[3] {
					new Point(position.Left + (position.Width / 2), position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			case Angle.TopRight:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Right, position.Bottom)
				};
			case Angle.BottomLeft:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			case Angle.BottomCenter:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Left + (position.Width / 2), position.Bottom)
				};
			case Angle.BottomRight:
				return new Point[3] {
					new Point(position.Right, position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			default:
				throw new NotImplementedException(Utils.GetEnumName(triangleAngle) + " Not yet implemented.");
			}
		}


		public override ShapeClickAction GetShapeActionByPoint(GraphicsPath path, Point point)
		{
			if (IsPointOverShape(path, point))
			{
				// Determine if not overlapping border, and drag. otherwise resize.
				path.Reset();
				path.AddPolygon(GetPointsByAngle(angle, Rectangle.Inflate(Position, -EDGE_WIDTH, -EDGE_WIDTH)));
				path.Flatten();
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
			Point[] points = GetPointsByAngle(angle, Position);
			path.AddPolygon(points);
			return path.IsVisible(point);
		}
	}
}
