using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	static public class Grid
	{
		public const int DEFAULT_SIZE = 10;
		static public Size GridSize = new Size(DEFAULT_SIZE, DEFAULT_SIZE);
		
		static public bool DrawGrid = true;
		static public bool SnapLocationToGrid = true;
		static public bool SnapSizeToGrid = true;

		static public void Draw(Canvas canvas, PaintEventArgs e)
		{
			if (!DrawGrid || GridSize.Width <= 0 || GridSize.Height <= 0)
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
		
		static public int SnapToGrid(int num, bool snap = true)
		{
			if (snap)
				return SnapToGrid(num, GridSize.Width);
			else
				return num;
		}

		static private int SnapToGrid(int num, int gridSize)
		{
			if (gridSize == 0)
				return num;
			
			return gridSize * (int)Math.Round((float)num / gridSize);
		}
	}
}
