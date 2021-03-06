﻿using System;
using System.Collections.Generic;
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

		/// <summary>
		/// For XML Serialization only
		/// </summary>
		public Square() : this(0, 0, 0, 0, DEFAULT_COLOR)
		{
		}

		public Square(int x, int y, int width, int height, Color color) : base(x, y, width, height, color)
		{
		}

		public override void DrawShape(Graphics graphics, Rectangle pos)
		{
			graphics.FillRectangle(Pen.Brush, pos);
		}

		public override void DrawBorder(Graphics graphics, Rectangle pos)
		{
			graphics.DrawRectangle(Pen, pos);
		}

		public override ShapeClickAction GetShapeActionByPoint(GraphicsPath path, Point point)
		{
			if (IsPointOverShape(path, point))
			{
				// Determine if not overlapping border, and drag. otherwise resize.
				path.Reset();
				path.AddRectangle(Rectangle.Inflate(Position, -Shape.EDGE_WIDTH, -Shape.EDGE_WIDTH));
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
			path.AddRectangle(Position);
			return path.IsVisible(point);
		}
	}
}
