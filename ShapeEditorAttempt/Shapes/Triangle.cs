using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ShapeEditorAttempt
{
	public partial class Triangle : Shape
	{

		new public const string NAME = "Triangle";
		new public const ShapeType TYPE = ShapeType.Triangle;
		new public const int EDGE_WIDTH = 8;
		override public string Name { get { return NAME; } }
		override public ShapeType Type { get { return TYPE; } }
		override public int EdgeWidth { get { return EDGE_WIDTH; } }
		
		private const AngleEnum DEFAULT_ANGLE = AngleEnum.TopLeft;
		public AngleEnum Angle { get; set; } = DEFAULT_ANGLE;

		/// <summary>
		/// For XML Serialization only
		/// </summary>
		public Triangle() : this(0, 0, 0, 0, DEFAULT_COLOR)
		{
		}

		public Triangle(int x, int y, int width, int height, Color color, AngleEnum angle)
			: base(x, y, width, height, color)
		{
			this.Angle = angle;
		}

		// Link any constructors without angles to the default angle
		public Triangle(int x, int y, int width, int height, Color color) : base (x, y, width, height, color)
		{
			this.Angle = DEFAULT_ANGLE;
		}

		public override void DrawShape(Graphics graphics, Rectangle position)
		{
			Point[] points = GetPointsByAngle(Angle, position);
			graphics.FillPolygon(Pen.Brush, points);
		}

		public override void DrawBorder(Graphics graphics, Rectangle position)
		{
			Point[] points = GetPointsByAngle(Angle, position);
			graphics.DrawPolygon(Pen, points);
		}

		public override ShapeClickAction GetShapeActionByPoint(GraphicsPath path, Point point)
		{
			if (IsPointOverShape(path, point))
			{
				// Determine if not overlapping border, and drag. otherwise resize.
				path.Reset();
				path.AddPolygon(GetPointsByAngle(Angle, Rectangle.Inflate(Position, -EDGE_WIDTH, -EDGE_WIDTH)));
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
			Point[] points = GetPointsByAngle(Angle, Position);
			path.AddPolygon(points);
			return path.IsVisible(point);
		}

		public void IncrementAngle()
		{
			Angle = (AngleEnum)Utils.WrapIncrement(Angle, 1, 0, AngleEnum.Length);
		}

		static private Point[] GetPointsByAngle(AngleEnum triangleAngle, Rectangle position)
		{
			switch (triangleAngle)
			{
			case AngleEnum.TopLeft:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Left, position.Bottom)
				};
			case AngleEnum.TopCenter:
				return new Point[3] {
					new Point(position.Left + (position.Width / 2), position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			case AngleEnum.TopRight:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Right, position.Bottom)
				};
			case AngleEnum.BottomLeft:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			case AngleEnum.BottomCenter:
				return new Point[3] {
					new Point(position.Left, position.Top),
					new Point(position.Right, position.Top),
					new Point(position.Left + (position.Width / 2), position.Bottom)
				};
			case AngleEnum.BottomRight:
				return new Point[3] {
					new Point(position.Right, position.Top),
					new Point(position.Left, position.Bottom),
					new Point(position.Right, position.Bottom)
				};
			default:
				throw new NotImplementedException(Utils.GetEnumName(triangleAngle) + " Not yet implemented.");
			}
		}
	}
}
