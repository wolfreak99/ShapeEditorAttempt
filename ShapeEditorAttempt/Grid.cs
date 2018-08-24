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
		public static int GridSize = 10;

		public static void Draw(PictureBox picCanvas, PaintEventArgs e)
		{
			DrawBackgroundGrid(picCanvas, e);
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
			return new Point(SnapToGrid(point.X), SnapToGrid(point.Y));
		}

		public static int SnapToGrid(int num)
		{
			return GridSize * (int)Math.Round((float)num / GridSize);
		}
	}
}
