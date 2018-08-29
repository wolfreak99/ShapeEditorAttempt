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
			BottomRight
		}
		
		private static Point[] GetPointsByAngle(Angle triangleAngle, Rectangle position)
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
				throw new NotImplementedException("Angle." + Enum.GetName(triangleAngle.GetType(), triangleAngle) + " Not yet implemented.");
			}
		}
		
		private Angle m_angle = Angle.TopLeft;
		private Point[] m_points;

		public Angle angle
		{
			get
			{
				return m_angle;
			}
			set
			{

				m_points = GetPointsByAngle(value, position);
				m_angle = value;
			}
		}

		public Point[] points
		{
			get
			{
				if (m_points == null)
				{
					m_points = GetPointsByAngle(angle, position);
				}
				return m_points;
			}
			private set
			{
				if (value.Length != 3)
				{
					throw new InvalidOperationException("Array length must be 3. (Try updating Triangle.angle, which changes this automatically)");
				}
				m_points = value;
			}
		}

		public void IncrementAngle()
		{
			angle = angle + 1 % Enum.GetValues(typeof(Angle)).Length;
		}

		public Triangle(int x, int y, int width, int height, Color color, Angle angle) : base(x, y, width, height, color)
		{
			this.angle = angle;
		}

		public Triangle(int x, int y, int width, int height, Color color) : this(x, y, width, height, color, Angle.TopLeft)
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

			graphics.FillPolygon(pen.Brush, points);
		}

		public override ShapeClickAction GetPointOverShapeAction(GraphicsPath path, Point point)
		{
			if (IsPointOverShape(path, point))
			{
				// Determine if not overlapping border, and drag. otherwise resize.
				path.Reset();
				path.AddPolygon(GetPointsByAngle(angle, Rectangle.Inflate(position, -Shape.EDGE_WIDTH, -Shape.EDGE_WIDTH)));
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
			return path.IsVisible(point);
		}

		public override string GetShapeName()
		{
			return "Triangle";
		}

		public override Shapes GetShapeType()
		{
			return Shapes.Triangle;
		}
	}
}
