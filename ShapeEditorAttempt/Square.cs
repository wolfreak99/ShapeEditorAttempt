using System;
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
			Pen p = new Pen(color, 1);
			Rectangle pos = position;
			switch (Form1.clickedShapeAction)
			{
				case ShapeClickAction.Drag:
					pos = PreviewDragOffset();
					break;
				case ShapeClickAction.Resize:
					pos = PreviewResizeOffset();
					break;
			}

			graphics.FillRectangle(p.Brush, pos);
		}

		public override ShapeClickAction GetPointOverShapeAction(Point point)
		{
			// TODO: Are new GraphicsPaths intensive? Are adding rectangles?
			// If both, create internal graphicspaths with AddRectangle, and reference IsVisible.
			// If not, Create either global or local GraphicsPath and add shapes and reset them.

			GraphicsPath path = new GraphicsPath(FillMode.Alternate);
			
			// Determine if not overlappint border, and drag.
			path.AddRectangle(Rectangle.Inflate(position, -Shape.EDGE_WIDTH, -Shape.EDGE_WIDTH));
			if (path.IsVisible(point))
				return ShapeClickAction.Drag;

			path.Reset();
			// Determine if overlapping border, and resize.
			path.AddRectangle(position);
			if (path.IsVisible(point))
			{
				path.Dispose();
				return ShapeClickAction.Resize;
			}

			path.Dispose();
			return ShapeClickAction.None;
		}
		

		protected override string GetShapeName()
		{
			return "Square";
		}


	}
}
