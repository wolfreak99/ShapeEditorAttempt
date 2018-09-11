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
		new public const string NAME = "Circle";
		new public const ShapeType TYPE = ShapeType.Circle;
		override public string Name { get { return NAME; } }
		override public ShapeType Type { get { return TYPE; } }

		/// <summary>
		/// For XML Serialization only
		/// </summary>
		public Circle() : this(0, 0, 0, 0, DEFAULT_COLOR)
		{
		}

		public Circle(int x, int y, int width, int height, Color color) : base(x, y, width, height, color)
		{
		}

		public override void DrawShape(Graphics graphics, Rectangle position)
		{
			graphics.FillEllipse(Pen.Brush, position);
		}

		public override void DrawBorder(Graphics graphics, Rectangle position)
		{
			graphics.DrawEllipse(Pen, position);
		}

		public override ShapeClickAction GetShapeActionByPoint(GraphicsPath path, Point point)
		{
			if (IsPointOverShape(path, point))
			{
				if (KeyboardController.IsMoveDown)
					return ShapeClickAction.Drag;

				// Determine if not overlapping border, and drag. otherwise resize.
				path.Reset();
				path.AddEllipse(Rectangle.Inflate(Position, -Shape.EDGE_WIDTH, -Shape.EDGE_WIDTH));
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
			path.AddEllipse(Position);
			return path.IsVisible(point);
		}
	}
}
