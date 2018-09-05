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
		static public bool DrawGrid = true;
		static public bool SnapLocationToGrid = true;
		static public bool SnapSizeToGrid = true;

		static public void Draw(Canvas canvas, PaintEventArgs e)
		{
			if (!DrawGrid)
				return;

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
		static public Point SnapToGrid(Point point, bool snap = true)
		{
			if (snap)
				return new Point(SnapToGrid(point.X, GridSize.Width), SnapToGrid(point.Y, GridSize.Height));
			else
				return point;
		}

		static public Size SnapToGrid(Size size, bool snap = true)
		{
			if (snap)
				return new Size(SnapToGrid(size.Width, GridSize.Width), SnapToGrid(size.Height, GridSize.Height));
			else
				return size;
		}

		static private int SnapToGrid(int num, int gridSize)
		{
			return gridSize * (int)Math.Round((float)num / gridSize);
		}
	}
}
