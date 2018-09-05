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
		override public string Name { get { return NAME; } }
		override public ShapeType Type { get { return TYPE; } }
		
		public new const int EDGE_WIDTH = 8;

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

		public override void Draw(Canvas sender, Graphics graphics)
		{
			Point[] newPoints = (ClickData.Shape == this) ? GetPointsByAngle(angle, PreviewOffset(position, ClickData.Action)) : points;
			graphics.FillPolygon(pen.Brush, newPoints);

			// Create outline
			if (KeyboardController.IsControlDown)
			{
				var prevColor = color;
				color = Utils.ColorSetHsv(prevColor.GetHue(), prevColor.GetSaturation() + 10, prevColor.GetBrightness() + 10);
				m_pen.Width = EDGE_WIDTH;
				graphics.DrawPolygon(pen, GetPointsByAngle(angle, PreviewOffset(position.InflatedBy(-EDGE_WIDTH / 2, -EDGE_WIDTH / 2), ClickData.Action)));
				color = prevColor;
			}
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
				throw new NotImplementedException(Utils.GetEnumName(triangleAngle) + " Not yet implemented.");
			}
		}


		public override ShapeClickAction GetShapeActionByPoint(GraphicsPath path, Point point)
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
	}
}
