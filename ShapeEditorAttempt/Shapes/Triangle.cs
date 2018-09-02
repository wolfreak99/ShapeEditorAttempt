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

		public new const Shapes TYPE = Shapes.Triangle;
		public new const string NAME = "Triangle";

		public new const int EDGE_WIDTH = 12;

		private const Angle DEFAULT_ANGLE = Angle.TopCenter;

		private Angle m_angle = DEFAULT_ANGLE;

		public Angle angle
		{
			get
			{
				return m_angle;
			}
			set
			{
				m_angle = value;
			}
		}

		public Point[] points
		{
			get
			{
				return GetPointsByAngle(angle, position);
			}
		}
		public void IncrementAngle()
		{
			angle = (Angle)Utils.WrapIncrement(angle, 1, 0, Angle.Length);
			//angle = (Angle)(((int)angle + 1) % (int)Angle.Length);
		}

		public Triangle(int x, int y, int width, int height, Color color, Angle angle) : base(x, y, width, height, color)
		{
			this.angle = angle;
		}

		// Link any constructors without angles to the default angle
		public Triangle(int x, int y, int width, int height, Color color) : base (x, y, width, height, color)
		{
			this.angle = DEFAULT_ANGLE;
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
			var newPoints = GetPointsByAngle(angle, pos);
			graphics.FillPolygon(pen.Brush, newPoints);
		}


		static private Point[] GetPointsByAngle(Angle triangleAngle, Rectangle position)
		{
			switch (triangleAngle)
			{
			case Angle.TopLeft:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			case Angle.TopCenter:
				return new Point[3] {
					new Point(position.Left + (position.Width / 2), position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			case Angle.TopRight:
				return new Point[3] {
					new Point(position.Right, position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			case Angle.BottomLeft:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Left, position.Bottom)
				};
			case Angle.BottomCenter:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Left + (position.Width / 2), position.Bottom)
				};
			case Angle.BottomRight:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Right, position.Bottom)
				};
			default:
				throw new NotImplementedException("Angle." + (Enum.GetNames(triangleAngle.GetType()))[(int)triangleAngle] + " Not yet implemented.");
			}
		}


		public override ShapeClickAction GetPointOverShapeAction(GraphicsPath path, Point point)
		{
			if (IsPointOverShape(path, point))
			{
				// Determine if not overlapping border, and drag. otherwise resize.
				path.Reset();
				path.AddPolygon(GetPointsByAngle(angle, Rectangle.Inflate(position, -EDGE_WIDTH, -EDGE_WIDTH)));
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
			path.AddPolygon(points);
			path.Flatten();
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
