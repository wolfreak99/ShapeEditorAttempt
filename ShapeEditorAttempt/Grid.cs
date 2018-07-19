using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public static class Grid
	{
		public static Shape[] shapes = new Shape[]{new Square(10, 20, 30, 30, Color.Blue), new Square(50, 60, 20, 10, Color.Red)};
		public static int GridSize = 10;

		public static void Draw(PictureBox picCanvas, PaintEventArgs e)
		{
			DrawBackgroundGrid(picCanvas, e);
			
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			
			foreach (var s in shapes)
			{
				s.Draw(e.Graphics);
			}
		}

		public static void DrawBackgroundGrid(PictureBox canvas, PaintEventArgs e)
		{
			var w = canvas.ClientSize.Width;
			var h = canvas.ClientSize.Height;
			var pen = Pens.Gray;
			
			for (int x = GridSize; x < w; x += GridSize)
			{
				e.Graphics.DrawLine(pen, x, 0, x, h);
			}
			for (int y = GridSize; y < h; y += GridSize)
			{
				e.Graphics.DrawLine(pen, 0, y, w, y);
			}
		}

		// Snap to the nearest grid point.
		public static Point SnapToGrid(Point point)
		{
			int x = GridSize * (int)Math.Round((float)point.X / GridSize);
			int y = GridSize * (int)Math.Round((float)point.Y / GridSize);
			return new Point(x, y);
		}
	}
}
