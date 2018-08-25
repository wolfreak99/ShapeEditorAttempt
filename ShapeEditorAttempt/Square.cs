﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeEditorAttempt
{
	public class Square : Shape
	{
		public Square(int x, int y, int width, int height, Color color) : base(x, y, width, height, color)
		{
		}

		public override void Draw(Graphics graphics)
		{
			Rectangle pos = PreviewOffset();

			graphics.FillRectangle(pen.Brush, pos);
		}

		public override ShapeClickAction GetPointOverShapeAction(GraphicsPath path, Point point)
		{
			// Determine if not overlappint border, and drag.
			path.Reset();
			path.AddRectangle(Rectangle.Inflate(position, -Shape.EDGE_WIDTH, -Shape.EDGE_WIDTH));
			if (path.IsVisible(point))
				return ShapeClickAction.Drag;

			// Determine if overlapping border, and resize.
			path.Reset();
			path.AddRectangle(position);
			if (path.IsVisible(point))
				return ShapeClickAction.Resize;

			return ShapeClickAction.None;
		}
		

		protected override string GetShapeName()
		{
			return "Square";
		}


	}
}
