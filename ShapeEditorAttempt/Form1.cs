using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public partial class Form1 : Form
	{
		bool mouseIsDown = false;

		Shape clickedShape = null;
		Point clickedOrigin = Point.Empty;

		
		public Form1()
		{
			InitializeComponent();
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			Grid.Draw(Canvas, e);
		}

		private void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (!mouseIsDown)
				return;

			if (clickedShape != null)
			{
				clickedShape.ApplyMoveOffset();
				clickedShape = null;
				clickedOrigin = Point.Empty;
			}

			Canvas.Invalidate();
			mouseIsDown = false;
			
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (!mouseIsDown)
				return;
			
			// Todo: Copy over the moving mechanics a little better.
			if (Grid.SnapToGrid(e.Location) == Grid.SnapToGrid(clickedOrigin) || clickedShape == null)
				return;

			Point moveTo = Point.Empty;
			moveTo.X = clickedOrigin.X - Grid.SnapToGrid(e.Location).X;
			moveTo.Y = clickedOrigin.Y - Grid.SnapToGrid(e.Location).Y;

			if (moveTo == Point.Empty)
				return;

			clickedShape.moveOffset = moveTo;

			Canvas.Invalidate();
		}

		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			// Only run during initial press
			if (mouseIsDown)
				return;

			int i = 0;
			foreach (Shape s in Grid.shapes)
			{
				if (s.IsPointOverShape(e.Location))
				{
					clickedShape = s;
					clickedOrigin = Grid.SnapToGrid(e.Location);
				}
				i++;
			}

			mouseIsDown = true;
			//throw new NotImplementedException();
		}

	}
}
