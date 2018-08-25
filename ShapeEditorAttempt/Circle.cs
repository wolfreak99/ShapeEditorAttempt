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
		public Circle(int x, int y, int width, int height, Color color) : base(x, y, width, height, color)
		{
		}

		public override void Draw(Graphics graphics)
		{
			Rectangle pos = PreviewOffset();
			graphics.FillEllipse(pen.Brush, pos);
		}

		public override ShapeClickAction GetPointOverShapeAction(GraphicsPath path, Point point)
		{
			// TODO: Are new GraphicsPaths intensive? Are adding rectangles?
			// If both, create internal graphicspaths with AddRectangle, and reference IsVisible.
			// If not, Create either global or local GraphicsPath and add shapes and reset them.
			
			// Determine if not overlapping border, and drag.
			path.Reset();
			path.AddEllipse(Rectangle.Inflate(position, -Shape.EDGE_WIDTH, -Shape.EDGE_WIDTH));
			if (path.IsVisible(point))
				return ShapeClickAction.Drag;

			// Determine if overlapping border, and resize.
			path.Reset();
			path.AddEllipse(position);
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
