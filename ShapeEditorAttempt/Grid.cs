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
	static public class Grid
	{
		static public Size GridSize = new Size(10, 10);

		static public void Draw(PictureBox picCanvas, PaintEventArgs e)
		{
			DrawBackgroundGrid(picCanvas, e);
		}

		static public void DrawBackgroundGrid(PictureBox canvas, PaintEventArgs e)
		{
			var w = canvas.ClientSize.Width;
			var h = canvas.ClientSize.Height;
			var pen = Pens.Gray;
			
			for (int x = GridSize.Width; x < w; x += GridSize.Width)
			{
				e.Graphics.DrawLine(pen, x, 0, x, h);
			}
			for (int y = GridSize.Height; y < h; y += GridSize.Height)
			{
				e.Graphics.DrawLine(pen, 0, y, w, y);
			}
		}

		// Snap to the nearest grid point.
		static public Point SnapToGrid(Point point)
		{
			return new Point(SnapToGrid(point.X, GridSize.Width), SnapToGrid(point.Y, GridSize.Height));
		}
		static public Size SnapToGrid(Size size)
		{
			return new Size(SnapToGrid(size.Width, GridSize.Width), SnapToGrid(size.Height, GridSize.Height));
		}

		static public int SnapToGrid(int num, int gridSize)
		{
			return gridSize * (int)Math.Round((float)num / gridSize);
		}
	}
}
