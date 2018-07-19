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
			Rectangle pos = moveOffset != Point.Empty ? PreviewMoveOffset() : position;
			graphics.FillRectangle(p.Brush, pos);
		}

		public override bool IsPointOverShape(Point point)
		{
			GraphicsPath path = new GraphicsPath(FillMode.Alternate);
			path.AddRectangle(position);
			return path.IsVisible(point);
		}

		protected override string GetShapeName()
		{
			return "Square";
		}


	}
}
