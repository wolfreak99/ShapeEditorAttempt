using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class SelectorTool : ToolBase
	{
		private Pen OutlinePen;
		
		private Rectangle selectedRectangle = new Rectangle();
		private Shape[] selectedShapes = null;
		private Point mouseDownLocation = new Point();

		public SelectorTool() : base()
		{
			OutlinePen = new Pen(Brushes.Blue);
			OutlinePen.Width = 2;
			OutlinePen.Color = Color.FromArgb(128, OutlinePen.Color.R, OutlinePen.Color.G, OutlinePen.Color.B);
			OutlinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
		}
		
		private void SetSelectedRectangle(Point beginPoint, Point endPoint)
		{
			Point a = Utils.Min(beginPoint, endPoint);
			Point b = Utils.Max(beginPoint, endPoint);

			var newRect = Rectangle.FromLTRB(a.X, a.Y, b.X, b.Y);
			// Determine if there is a need to update shapes
			if (selectedRectangle == newRect)
				return;

			selectedRectangle = newRect;
			selectedShapes = Canvas.Instance.GetShapesByRectangle(selectedRectangle);

			// Update borders of selected shapes
			var array = Canvas.Instance.layer.ToArray();
			foreach (var s in array)
			{
				s.BorderVisible = false;
			}
			foreach (var s in selectedShapes)
			{
				s.BorderVisible = true;
			}

			// This may be needed
			//Canvas.Instance.Invalidate();
		}

		private void ClearSelectedRectangle()
		{
			selectedRectangle = Rectangle.Empty;
			mouseDownLocation = Point.Empty;
		}

		public override void OnMouseDown(object sender, MouseEventArgs e)
		{
			// Set initial press
			if (!MouseWasDown)
				mouseDownLocation = e.Location;

			SetSelectedRectangle(mouseDownLocation, e.Location);
		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (!MouseIsDown)
				return;

			SetSelectedRectangle(mouseDownLocation, e.Location);
		}

		public override void OnMouseUp(object sender, MouseEventArgs e)
		{
			
		}

		public override void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
			ClearSelectedRectangle();

			Canvas.Instance.Invalidate();
		}

		public override void OnPaint(object sender, PaintEventArgs e)
		{
			if (selectedRectangle.IsEmpty)
				return;

			Pen p = new Pen(Brushes.Blue);
			p.Width = 2;
			p.Color = Color.FromArgb(128, p.Color.R, p.Color.G, p.Color.B);
			p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			e.Graphics.DrawRectangle(p, selectedRectangle);
		}

		public override void OnProcessKeys(KeyEventArgs e, bool isDown)
		{
			switch (e.KeyCode)
			{
			case Keys.Delete:
				if (selectedShapes != null)
				{
					foreach (var s in selectedShapes)
					{
						SharedActions.RemoveShape(s);
					}
				}
				break;
			}
		}

		public override void OnUnloadTool()
		{
			ClearSelectedRectangle();
			selectedShapes = null;
		}
	}
}
